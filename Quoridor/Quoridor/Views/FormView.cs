using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Quoridor.Models.Map;
using Quoridor.Views.ViewModels;
using Quoridor.Models.Shared;
using Quoridor.Models.Players;
using Quoridor.Controllers;

namespace Quoridor.Views
{
    class FormView: Form
    {
        private Map map;
        private Player currentPlayer;
        GameController _controller;
        public FormView(GameController controller, Map map = null) {
            if (controller == null) throw new Exception("No controller for the view");
            _controller = controller;
            this.ClientSize = new Size(Settings.MAPSIZE, Settings.MAPSIZE);
            this.Name = "QuoridorViewForm";
            this.Text = "Quoridor";
            this.map = map;
            this.Shown += FormView_Shown;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void FormView_Shown(object sender, EventArgs e)
        {
            List<PositionButton> cellButtons = new List<PositionButton>();
            foreach (var cell in map.Cells)
            {
                string name;
                int x, y, row = cell.Position.RowPosition, col = cell.Position.ColumnPosition;
                x = col * Settings.CELLSIZE + col * Settings.BLOCKSIZE;
                y = row * Settings.CELLSIZE + row * Settings.BLOCKSIZE;
                name = $"cellButton_{row}_{col}";
                var playerInCell = map.Players.
                    Where(pl => pl.Position.RowPosition == row && pl.Position.ColumnPosition == col).
                    FirstOrDefault();
                PositionButton button = CreateCellButton(name, cell.Position, x, y, playerInCell?.Color);
                cellButtons.Add(button);
            }
            this.Controls.AddRange(cellButtons.ToArray());
            List<BlockButton> blockButtons = new List<BlockButton>();
            for (int i = 0; i < Settings.ROWS * 2 - 1; i++)
            {
                bool isHorizontal = true;
                int columns = Settings.COLUMNS;
                if (i % 2 == 0) {
                    columns -= 1;
                    isHorizontal = false;
                }
                for (int j = 0; j < columns; j++)
                {
                    BlockButton block;
                    string name = $"blockButton{ (isHorizontal ? 'H' : 'V') }_{ i }_{ j }";
                    int x, y;
                    if(!isHorizontal)
                    {
                        x = Settings.CELLSIZE + j * (Settings.CELLSIZE + Settings.BLOCKSIZE);
                        y = (i / 2)* (Settings.CELLSIZE + Settings.BLOCKSIZE);
                    }
                    else
                    {
                        x = j * (Settings.CELLSIZE + Settings.BLOCKSIZE);
                        y = Settings.CELLSIZE + (i/2) * (Settings.CELLSIZE + Settings.BLOCKSIZE);
                    }
                    block = CreateBlockButton(name, new Position { ColumnPosition = j, RowPosition = i }, x, y, isHorizontal);
                    blockButtons.Add(block);
                }
            }
            this.Controls.AddRange(blockButtons.ToArray());

            currentPlayer = map.Players.First();
            var standingCell = GetCellButtonByPosition(currentPlayer.Position);
            standingCell.IsPlayersTurn = true;
        }

        private PositionButton CreateCellButton(string name, Position position, int x, int y, Color? playerColor = null)
        {
            CellButton button = new CellButton() {
                Location = new Point(x, y), Position = position,
                Size = new Size(Settings.CELLSIZE, Settings.CELLSIZE),
                Name = name, Text = string.Empty, BackColor = Color.Bisque
            };
            if (playerColor != null)
            {
                button.IsContainsPlayer = true;
                button.PlayerColor = (Color)playerColor;
            }
            else button.Click += this.OnCellButtonClick;
            return button;
        }

        private BlockButton CreateBlockButton(string name, Position position, int x, int y, bool isHorizontal)
        {
            Size size = new Size();
            size.Width = isHorizontal ? Settings.CELLSIZE : Settings.BLOCKSIZE;
            size.Height = !isHorizontal ? Settings.CELLSIZE : Settings.BLOCKSIZE;
            BlockButton button = new BlockButton()
            {
                Name = name, Text = string.Empty,
                Position = position, Location = new Point(x, y),
                Size = size, IsHorizontal = true,
                BackColor = Color.Transparent
            };
            button.Click += this.OnBlockButtonClick;
            return button;
        }
        private void EndTurn()
        {
            var playerId = map.Players.IndexOf(currentPlayer);
            int nextPlayerId = playerId + 1 < map.Players.Count ? playerId + 1 : 0;

            var standingCellPrev = GetCellButtonByPosition(currentPlayer.Position);
            standingCellPrev.IsPlayersTurn = false;
            standingCellPrev.Refresh();

            currentPlayer = map.Players.ElementAt(nextPlayerId);
            var standingCell = GetCellButtonByPosition(currentPlayer.Position);
            standingCell.IsPlayersTurn = true;
            standingCell.Refresh();

        }
        private void OnCellButtonClick(object sender, EventArgs e)
        {
            CellButton cell = sender as CellButton;
            var result = _controller.PostPlayerMove(map, currentPlayer, cell.Position);
            if(!result.IsSuccess)
            {
                ShowPopup("You can't move there due to rules", "Mistake");
                return;
            }
            map = result.Value;
            MovePlayer(currentPlayer, cell);
            EndTurn();
        }

        private void OnBlockButtonClick(object sender, EventArgs e)
        {
            BlockButton block = sender as BlockButton;
            var placeOfSecondPart = this.GetSecondBlockPlacePosition(block.Position, block.IsHorizontal);

            var result = _controller.PostPlayerBlock(map, currentPlayer, new[] { block.Position, placeOfSecondPart });
            if (!result.IsSuccess)
            {
                ShowPopup("You can't place block there due to rules", "Mistake");
                return;
            }
            map = result.Value;
            
        }
        private void ShowPopup(string message, string caption = "INFO")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void MovePlayer(Player player, CellButton moveCellButton)
        {
            var standingCell = GetCellButtonByPosition(player.Position);
            standingCell.IsContainsPlayer = false;
            standingCell.Enabled = true;
            standingCell.Refresh();

            player.Position = moveCellButton.Position;

            moveCellButton.IsContainsPlayer = true;
            moveCellButton.PlayerColor = player.Color;
            moveCellButton.Refresh();
        }

        private void PlaceBlock(Position positionStart, Position positionEnd)
        {
            var startBlock = GetBlockButtonByPosition(positionStart);
            
        }

        private Position GetSecondBlockPlacePosition(Position blockPosition, bool isHorizontal)
        {
            Position result = new Position
            {
                ColumnPosition = blockPosition.ColumnPosition,
                RowPosition = blockPosition.RowPosition
            };
            if (!isHorizontal)
                result.RowPosition += result.RowPosition + 1 > 2 * (Settings.ROWS - 1) ? -1 : 1;
            else
                result.ColumnPosition += result.ColumnPosition + 1 > Settings.COLUMNS ? -1 : 1;
            return result;
        }

        private CellButton GetCellButtonByPosition(Position position)
        {
            string name = $"cellButton_{position.RowPosition}_{position.ColumnPosition}";
            return Controls.Find(name, false).FirstOrDefault() as CellButton;
        }

        private BlockButton GetBlockButtonByPosition(Position position)
        {
            string name = $"blockButton_{position.RowPosition}_{position.ColumnPosition}";
            return Controls.Find(name, false).FirstOrDefault() as BlockButton;
        }
    }
}

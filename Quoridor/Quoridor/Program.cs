using Quoridor.Models.Map;
using Quoridor.Models.MapObjects;
using Quoridor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quoridor.Models.Shared;
using Quoridor.Models.Players;
using Quoridor.Controllers;

namespace Quoridor
{
    class Program
    {
        static Map Initialize2PlayersMap()
        {
            Map map = new Map();
            Player[] players = {
                new Player {
                    Name = "A", BlocksRemaining = 10, Color = System.Drawing.Color.Red,
                    Position = new Position { ColumnPosition = 4, RowPosition = 0 }
                },
                new Player {
                    Name = "B", BlocksRemaining = 10, Color = System.Drawing.Color.Blue ,
                    Position = new Position { ColumnPosition = 4, RowPosition = 8 }
                }
            };
            for(int i = 0; i < Settings.ROWS; i++)
            {
                for (int j = 0; j < Settings.COLUMNS; j++)
                {
                    Cell cell = new Cell { Position = new Position { ColumnPosition = j, RowPosition = i } };
                    map.Cells.Add(cell);
                }
            }
            map.Players.AddRange(players);
            return map;
        }

        static void Main(string[] args)
        {
            GameController controller = new GameController();
            Application.EnableVisualStyles();
            Application.Run(new FormView(controller, Initialize2PlayersMap()));
        }
    }
}

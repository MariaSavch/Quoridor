using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoridor.Views.ViewModels
{
    class CellButton : PositionButton
    {
        public bool IsContainsPlayer { get; set; } = false;
        public bool IsPlayersTurn { get; set; } = false;

        public Color PlayerColor { get; set; }

        private static Color DefaultColor = Color.Bisque;
        public CellButton() { }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath grpath = new GraphicsPath();
            if (!IsContainsPlayer) {
                BackColor = DefaultColor;
                FlatStyle = FlatStyle.Standard;
                Enabled = true;
                grpath.AddRectangle(new RectangleF(0, 0, ClientSize.Width, ClientSize.Height));
                this.Region = new Region();
                base.OnPaint(pevent);
                return;
            }
            grpath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height );
            if(IsPlayersTurn)
            {
                grpath.AddString("T", Font.FontFamily, 0, 15, new PointF(ClientSize.Width/2 - 7.5f, ClientSize.Height/2 - 7.5f), new StringFormat());
            }
            this.Region = new Region(grpath);
            BackColor = PlayerColor;
            FlatStyle = FlatStyle.Flat;
            Enabled = false;
            base.OnPaint(pevent);
        }
    }
}

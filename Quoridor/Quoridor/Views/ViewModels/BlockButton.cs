using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoridor.Views.ViewModels
{
    class BlockButton : PositionButton
    {
        public bool IsHorizontal { get; set; }
        public bool IsBlocked { get; set; }
        public BlockButton(bool isHorizontal = true)
        {
            IsHorizontal = isHorizontal;
            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.Transparent;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
        }
    }
}

using Quoridor.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoridor.Views.ViewModels
{
    class PositionButton : Button
    {
        public Position Position { get; set; }

        public PositionButton() { }
    }
}

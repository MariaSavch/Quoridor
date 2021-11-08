using Quoridor.Models.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Models.Players
{
    class Player
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public int BlocksRemaining { get; set; }
        public Position Position { get; set; }

        public Position[] WinningPositions { get; set; }
    }
}

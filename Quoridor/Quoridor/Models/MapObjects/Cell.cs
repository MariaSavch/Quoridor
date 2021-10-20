using Quoridor.Models.Players;
using Quoridor.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Models.MapObjects
{
    class Cell
    {
        public bool BlockTop { get; set; }
        public bool BlockBottom { get; set; }
        public bool BlockRight { get; set; }
        public bool BlockLeft { get; set; }

        public Position Position { get; set; }

    }
}

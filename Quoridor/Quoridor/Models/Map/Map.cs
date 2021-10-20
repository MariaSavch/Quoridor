using Quoridor.Models.MapObjects;
using Quoridor.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Models.Map
{
    using Cells = List<Cell>;
    using Blocks = List<Block>;
    using Players = List<Player>;
    class Map
    {
        public Cells Cells { get; set; } = new Cells();
        public Blocks Blocks { get; set; } = new Blocks();

        public Players Players { get; set; } = new Players();
    }
}

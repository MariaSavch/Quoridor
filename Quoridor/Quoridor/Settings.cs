using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor
{
    public static class Settings
    {
        public static int ROWS { get; } = 9;
        public static int COLUMNS { get; } = 9;

        public static int CELLSIZE { get; } = 75;
        public static int BLOCKSIZE { get; } = 25;
        public static int FENCESIZE { get; } = CELLSIZE * 2 + BLOCKSIZE;
        public static int MAPSIZE { get; } = 875;
        public static int PlayersCount { get; set; } = 2;
        public static int PlayersBlocks { get; set; } = 10;
    }
}

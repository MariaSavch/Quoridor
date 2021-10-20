using Quoridor.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Models.MapObjects
{

    
    class Block
    {
        public bool IsHorizontal { get; set; } = false;

        public Position Position { get; set; }
    }
}

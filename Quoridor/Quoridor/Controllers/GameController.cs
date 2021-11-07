using Quoridor.Models.Map;
using Quoridor.Models.Players;
using Quoridor.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Controllers
{
    class GameController
    {
        public Result<Map> PostPlayerMove(Map map, Player player, Position playerMove)
        {
            return new Result<Map>(true, map);
        }
        public Result<Map> PostPlayerBlock(Map map, Player player, Position blockPosition1, Position blockPosition2)
        {
            return new Result<Map>(true, map);
        }
    }
}

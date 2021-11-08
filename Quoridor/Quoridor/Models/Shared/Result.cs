using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Models.Shared
{
    class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T Value { get; private set; }

        public Result(bool success, T value) {
            IsSuccess = success;
            Value = value;
        }
    }
}

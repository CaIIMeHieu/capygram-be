﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace capygram.Common.Shared
{
    public class Result<T>
    {
        public Result() { }

        public Result(bool Success, ResultDetail Detail, T? Value)
        {
            result_detail = Detail;
            success = Success;
            value = Value;
        }

        public ResultDetail result_detail { get; set; }  
        public bool success { get; set; } 
        public T? value { get; set; }  

        public static Result<T> CreateResult(bool Success, ResultDetail Detail, T? Value)
            => new Result<T>(Success, Detail, Value);

        public static implicit operator Result<T>(T Value) => new Result<T>(true, new ResultDetail("200", "Success"), Value);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBNiuBi.Model
{
    public class ResponseResult : ResponseResult<object>
    {

    }
    /// <summary>
    /// 
    /// </summary>
    public class ResponseResult<T>
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public T data { get; set; }

        public ResponseResult<T> SetSuccess(bool success)
        {
            this.success = success;
            return this;
        }

        public ResponseResult<T> SetMsg(string msg)
        {
            this.msg = msg;
            return this;
        }

        public ResponseResult<T> SetData(T data)
        {
            this.data = data;
            return this;
        }

        public static ResponseResult<T> Fail()
        {
            return new ResponseResult<T>().SetSuccess(false);
        }

        public static ResponseResult<T> Ok()
        {
            return new ResponseResult<T>().SetSuccess(true);
        }
    }
}

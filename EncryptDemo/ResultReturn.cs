using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDemo
{
    class ResultReturn
    {
    }
    public class ResultObject<T>
    {

        public static ResultObject<T> GetResultObject(T Data, int error_code = 1)
        {
            ResultObject<T> ret = new ResultObject<T>();
            ret.error_code = error_code;
            ret.Data = Data;
            ret.msg = "";
            return ret;
        }

        public static ResultObject<T> GetResultObject(T Data, string msg, int error_code = 1)
        {
            ResultObject<T> ret = new ResultObject<T>();
            ret.error_code = error_code;
            ret.Data = Data;
            ret.msg = msg;
            return ret;
        }

        public T Data { get; set; }

        public string msg { get; set; }
        public int error_code { get; set; }
    }
    public class ResultList<T>
    {

        public static ResultList<T> GetResultList(List<T> Data, int error_code = 1)
        {
            ResultList<T> ret = new ResultList<T>();
            ret.error_code = error_code;
            ret.Data = Data;
            ret.msg = "";
            return ret;
        }
        public static ResultList<T> GetResultList(List<T> Data, string msg, int error_code = 1)
        {
            ResultList<T> ret = new ResultList<T>();
            ret.error_code = error_code;
            ret.Data = Data;
            ret.msg = msg;
            return ret;
        }
        public static ResultList<T> GetResultList(List<T> Data, string msg, int total, int PageIndex, int PageSize, int error_code = 1)
        {
            ResultList<T> ret = new ResultList<T>();
            ret.error_code = error_code;
            ret.Data = Data;
            ret.msg = msg;
            ret.total = total;
            ret.PageCount = PageIndex;
            ret.PageSize = PageSize;
            return ret;
        }
        public List<T> Data { get; set; }

        public string msg { get; set; }
        public int error_code { get; set; }
        public int total { get; set; }

        public int PageCount
        {
            get;

            set;

        }
        public int PageSize
        {
            get;
            set;
        }
    }
}

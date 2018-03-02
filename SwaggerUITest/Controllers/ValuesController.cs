using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwaggerUITest.Controllers
{
    /// <summary>
    /// 取值接口
    /// </summary>
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="id">参数id</param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Post请求值
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        /// <summary>
        /// 查询测试
        /// </summary>
        public string TestSearch([FromBody]string name)
        {
            return string.Format("value_{0}", name);
        }
    }
}
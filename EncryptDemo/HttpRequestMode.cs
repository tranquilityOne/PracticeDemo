﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncryptDemo
{
    /// <summary>
    /// http请求 类封装
    /// </summary>
    public class HttpRequestMode
    {

        /// <summary>
        /// 令牌
        /// </summary>
        public string token { set; get; }

        /// <summary>
        /// 数据 json 格式
        /// </summary>
        public string data { set; get; }

    }


}
﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public interface IHttpRequestFeature
    {
        Uri Url { get; }

        NameValueCollection Headers { get; }

        Stream Body { get; }
    }


    public interface IHttpResponseFeature
    {
        int StatusCode { get; set; }

        NameValueCollection Headers { get; }

        Stream Body { get; }

    }

}

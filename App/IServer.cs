﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }
}

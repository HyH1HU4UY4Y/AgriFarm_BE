﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Common
{
    public class DefaultResponse<T>
    {
        public T Data { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.Subscribe;

namespace SharedDomain.Entities.FarmComponents
{
    public class Site : BaseEntity
    {
        public string Name { get; set; }
        public string? Intro { get; set; }
        public string SiteKey { get; set; }
        public bool IsActive { get; set; } = false;

        public string? PaymentDetail { get; set; }

        public ICollection<Subscripton> Subscripts { get; set; }
        public ICollection<BaseComponent> Components { get; set; }
        public ICollection<CapitalState> Capitals { get; set; }


    }
}

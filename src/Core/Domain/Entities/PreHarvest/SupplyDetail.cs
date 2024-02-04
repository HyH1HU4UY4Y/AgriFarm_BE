﻿using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.PreHarvest
{
    public class SupplyDetail : BaseEntity
    {
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public Guid ComponentId { get; set; }
        public BaseComponent Component { get; set; }
    }
}

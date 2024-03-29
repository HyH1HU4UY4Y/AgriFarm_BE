﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.Base;

namespace SharedDomain.Entities.PreHarvest
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        [MaxLength(8000)]
        public string? Description { get; set; }
        public Guid? CreatedByFarmId {  get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public ICollection<SupplyDetail> Components { get; set; }

    }
}

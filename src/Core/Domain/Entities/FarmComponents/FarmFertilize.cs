﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedDomain.Entities.FarmComponents.Common;

namespace SharedDomain.Entities.FarmComponents
{
    public class FarmFertilize : BaseComponent
    {
        public FarmFertilize()
        {
            IsConsumable = true;
        }
        public decimal? UnitPrice { get; set; }
        public int Stock { get; set; } = 0; 
        public Guid? ReferenceId { get; set; }
        public ReferencedFertilize? Reference { get; set; }




    }
}

﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Entities.FarmComponents
{
    public class FarmWater : BaseComponent
    {
        private string _positionStr;
        public string? Position
        {
            get => _positionStr;
            set
            {
                _positionStr = value;

            }
        }

        [NotMapped]
        public List<PositionPoint> Positions
        {
            get => !string.IsNullOrWhiteSpace(_positionStr) ?
                JsonConvert.DeserializeObject<List<PositionPoint>>(_positionStr)!
                : new();
            set => _positionStr = JsonConvert.SerializeObject(value);
        }

        public string? FromSource { get; set; }
        public double? Acreage { get; set; }
    }
}

﻿using SharedDomain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SharedDomain.Entities.Risk
{
    public class RiskItem: BaseEntity
    {
        public Guid RiskMasterId { get; set; }
        public string? RiskItemTitle { get; set; }
        public int? RiskItemDiv { get; set; }
        public int? RiskItemType { get; set; }
        [StringLength(Int32.MaxValue)]
        public string? RiskItemContent { get; set; }
        public int? Must { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
        [JsonIgnore]
        public RiskMaster? RiskMaster { get; set; }
        public ICollection<RiskItemContent>? RiskItemContents { get; set; }
    }
}

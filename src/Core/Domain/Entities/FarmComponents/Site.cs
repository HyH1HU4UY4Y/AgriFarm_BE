using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedDomain.Entities.Base;
using SharedDomain.Entities.FarmComponents.Others;
using SharedDomain.Entities.Subscribe;

namespace SharedDomain.Entities.FarmComponents
{
    public class Site : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; } = "";
        public string SiteCode { get; set; }
        public bool IsActive { get; set; } = false;
        public string? AvatarImg { get; set; }
        public string? LogoImg { get; set; }

        public string? PaymentDetail { get; set; }

        private string _positionStr;
        [MaxLength(int.MaxValue)]
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
            get => !string.IsNullOrWhiteSpace(_positionStr)?
                JsonConvert.DeserializeObject<List<PositionPoint>>(_positionStr)! 
                : new();
            set => _positionStr = JsonConvert.SerializeObject(value);
        }

        public double? Acreage { get; set; }


        public ICollection<Subscripton> Subscripts { get; set; }
        public ICollection<BaseComponent> Components { get; set; }
        public ICollection<CapitalState> Capitals { get; set; }


    }
}

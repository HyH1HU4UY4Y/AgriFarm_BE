using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Defaults
{
    public sealed class EventQueue
    {
        public const string RegistFarmQueue= nameof(RegistFarmQueue);
        public const string InitFarmOwnerQueue= nameof(InitFarmOwnerQueue);
        public const string SupplyContractQueue= nameof(SupplyContractQueue);

        //replicate data queues section
        public const string UserReplicationQueue = nameof(UserReplicationQueue);
        public const string FarmReplicationQueue = nameof(FarmReplicationQueue);
        public const string SoilReplicationQueue = nameof(SoilReplicationQueue);
        public const string EquipmentReplicationQueue = nameof(EquipmentReplicationQueue);
        public const string SeedReplicationQueue = nameof(SeedReplicationQueue);
        public const string FertilizeReplicationQueue = nameof(FertilizeReplicationQueue);
        public const string PesticideReplicationQueue = nameof(PesticideReplicationQueue);
        public const string WaterReplicationQueue = nameof(WaterReplicationQueue);

        public const string SeasonReplicationQueue = nameof(SeasonReplicationQueue);


        //supply
        public const string PesticideSupplyingQueue = nameof(PesticideSupplyingQueue);
        public const string FertilizeSupplyingQueue = nameof(FertilizeSupplyingQueue);
        public const string SeedSupplyingQueue = nameof(SeedSupplyingQueue);
        public const string EquipmentSupplyingQueue = nameof(EquipmentSupplyingQueue);
        public const string LandSupplyingQueue = nameof(LandSupplyingQueue);

        //activity
        public const string TrainingDetailReplicationQueue = nameof(TrainingDetailReplicationQueue);
        public const string RiskMappingTrackingQueue = nameof(RiskMappingTrackingQueue);

    }
}

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
        public const string LandReplicationQueue = nameof(LandReplicationQueue);
        public const string EquipmentReplicationQueue = nameof(EquipmentReplicationQueue);
        public const string SeedReplicationQueue = nameof(SeedReplicationQueue);
        public const string FertilizeReplicationQueue = nameof(FertilizeReplicationQueue);
        public const string PesticideReplicationQueue = nameof(PesticideReplicationQueue);
        public const string WaterReplicationQueue = nameof(WaterReplicationQueue);
    }
}

using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Defaults.Converters
{
    public static class ItemConverterExtensions
    {
        public static string GetStringType<T>(this T component)
            where T : BaseComponent
        {
            switch (component)
            {
                case FarmSoil:
                    return ComponentTypeClientRoute.Soil;
                case FarmSeed:
                    return ComponentTypeClientRoute.Seed;
                case FarmWater:
                    return ComponentTypeClientRoute.Water;
                case FarmPesticide:
                    return ComponentTypeClientRoute.Pesticide;
                case FarmFertilize:
                    return ComponentTypeClientRoute.Pesticide;
                case FarmEquipment:
                    return ComponentTypeClientRoute.Equipment;
                default:
                    break;
            }

            return "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDomain.Defaults.Measures
{
    public class LengthUnit
    {
        public const string m2 = nameof(m2);
        public const string ha = nameof(ha);
        public const string km2 = nameof(km2);

        public static string[] All { get; } = 
        {
            m2,
            ha, 
            km2
        };
    }
}

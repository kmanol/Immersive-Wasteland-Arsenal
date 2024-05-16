using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmersiveWastelandArsenal
{
    [Flags] public enum OutputFlags { GECK = 1, TextStatic = 2, TextDynamic = 3 }

    public class Options
    {
        public OutputFlags Output;

        public Options(OutputFlags output)
        {
            this.Output = output;
        }
    }
}

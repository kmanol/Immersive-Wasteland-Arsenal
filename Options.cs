using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmersiveWastelandArsenal
{
    [Flags] public enum OutputFlags { GECK = 0, TextStatic = 1, TextDynamic = 2 }

    public class Options
    {
        public OutputFlags Output;

        public Options(OutputFlags output)
        {
            this.Output = output;
        }
    }
}

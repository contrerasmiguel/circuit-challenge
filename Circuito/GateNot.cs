using System.Collections.Generic;
using System.Linq;

namespace Circuit
{
    class GateNot : Gate
    {
        public GateNot(List<GateInput> input) : base(input)
        {
        }

        public override ConductorState Output
        {
            get
            {
                return ((input.First().State == ConductorState.High) ? ConductorState.Low
                    : (input.First().State == ConductorState.Low) ? ConductorState.High
                    : ConductorState.Disconnected);
            }
        }

        public override string ToString()
        {
            return "NOT => " + ((Output == ConductorState.High) ? "1"
                : (Output == ConductorState.Low) ? "0"
                : "Disconnected");
        }
    }
}

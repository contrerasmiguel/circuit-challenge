using System.Collections.Generic;
using System.Linq;

namespace Circuit
{
    class GateOr : Gate 
    {
        public GateOr(List<GateInput> input) : base(input)
        {
        }

        public override ConductorState Output
        {
            get
            {
                return (input.Where(conductor => conductor.State != ConductorState.Disconnected).Count() < 2)
                        ? ConductorState.Disconnected
                    : (input.Any(conductor => conductor.State == ConductorState.High))
                        ? ConductorState.High
                        : ConductorState.Low;
            }
        }

        public override string ToString()
        {
            return "OR  => " + ((Output == ConductorState.High) ? "1"
                : (Output == ConductorState.Low) ? "0"
                : "Disconnected");
        }
    }
}

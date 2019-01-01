using System.Collections.Generic;
using System.Linq;

namespace Circuit
{
    class GateXor : Gate
    {
        public GateXor(List<GateInput> input) : base(input)
        {
        }

        public override ConductorState Output
        {
            get
            {
                return (input.Where(conductor => conductor.State != ConductorState.Disconnected).Count() < 2)
                        ? ConductorState.Disconnected
                    : (input.Where(conductor => conductor.State != ConductorState.High).Count() % 2 != 0)
                        ? ConductorState.High
                        : ConductorState.Low;
            }
        }

        public override string ToString()
        {
            return "XOR => " + ((Output == ConductorState.High) ? "1"
                : (Output == ConductorState.Low) ? "0"
                : "Disconnected");
        }
    }
}

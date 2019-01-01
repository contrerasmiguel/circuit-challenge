using System.Collections.Generic;

namespace Circuit
{
    abstract class Gate
    {
        protected List<GateInput> input;

        public Gate(List<GateInput> input)
        {
            this.input = input;
        }

        public abstract ConductorState Output { get; }
    }
}

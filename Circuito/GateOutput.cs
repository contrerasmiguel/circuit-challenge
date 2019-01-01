using System.Collections.Generic;

namespace Circuit
{
    class GateOutput : Conductor
    {
        public Gate AssociatedGate { get; set; }

        public GateOutput()
        {  }

        public override ConductorState State
        {
            get
            {
                return AssociatedGate.Output;
            }
        }

        public override ConductorState GetState(List<Conductor> visitedConnections)
        {
            return AssociatedGate.Output;
        }

        public override string ToString()
        {
            return "░·░";
        }
    }
}

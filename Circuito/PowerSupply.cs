using System.Collections.Generic;

namespace Circuit
{
    class PowerSupply : Conductor
    {
        private bool enabled;

        public PowerSupply(bool enabled)
        {
            this.enabled = enabled;
        }

        public override ConductorState State
        {
            get
            {
                return (enabled) 
                    ? ConductorState.High 
                    : ConductorState.Low;
            }
        }

        public override ConductorState GetState(List<Conductor> visitedConnections)
        {
            return (enabled)
                ? ConductorState.High
                : ConductorState.Low;
        }

        public override string ToString()
        {
            return (enabled) ? " 1 " : " 0 ";
        }
    }
}

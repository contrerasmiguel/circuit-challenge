using System;
using System.Collections.Generic;
using System.Linq;

namespace Circuit
{
    enum ConductorState
    {
          High
        , Low
        , Disconnected
    };

    class Conductor : Component
    {
        public List<Conductor> Connections { get; }

        public Conductor()
        {
            Connections = new List<Conductor>();
        }

        public virtual ConductorState State
        {
            get
            {
                return GetState(new List<Conductor>());
            }
        }

        public virtual ConductorState GetState(List<Conductor> visitedConnections)
        {
            // We go through the nodes linked to the conductor until a PowerSupply
            // or GateOuput node is found, since they power the network.
            // This is a list of the nodes connected to the current node.
            // This list does not include current node or those node that have been already visited.
            List<Conductor> availableConnections = Connections.SkipWhile(c => c == null).Except(visitedConnections).ToList();

            // This is a list of the status of the conductors. Disconnected status are ignored.
            // This is used to detect short circuits.
            List<ConductorState> inputList =
                (from conn in availableConnections
                 select conn.GetState(visitedConnections.Concat(new List<Conductor> { this }).ToList()))
                .ToList().Where(state => state != ConductorState.Disconnected).ToList();

            // If the list of connections to a node equals to zero, then the node is disconnected.
            if (inputList.Count == 0)
            {
                return ConductorState.Disconnected;
            }
            else
            {
                ConductorState firstState = inputList.First();

                // Is there any node with two different inputs (HIGH and LOW at the same time)
                if (inputList.GetRange(1, inputList.Count - 1).Any(state => state != firstState))
                {
                    throw new Exception("Short circuit was found!");
                }
                else
                {
                    return inputList.First();
                }
            }
        }

        public override string ToString()
        {
            return " · ";
        }
    }
}

using System;
using System.Collections.Generic;

namespace Circuit
{
    class Components
    {
        private Component[,] components;

        public List<Gate> Gates { get; }

        public Components(char[][] rawInputData)
        {
            int 
                  numRows = rawInputData.Length + 1
                , numColumns = rawInputData[0].Length;

            components = new Component[numRows, numColumns];
            Gates = new List<Gate>();

            // Include power supplies
            for (int j = 0; j < numColumns; ++j)
            {
                components[0, j] = new PowerSupply(j % 2 == 0);
            }

            // Include rawInputData components
            for (int rawInputDataRow = 0, componentsRow = 1
                ; rawInputDataRow < (numRows - 1)
                ; ++rawInputDataRow, ++componentsRow)
            {
                for (int column = 0; column < numColumns; ++column)
                {
                    // Each component has its own class
                    switch (rawInputData[rawInputDataRow][column])
                    {
                        case 'C':
                            components[componentsRow, column] = new Conductor();
                            break;
                        case 'R':
                            components[componentsRow, column] = new Insulator();
                            break;
                        case 'P':
                            components[componentsRow, column] = new GateCase();
                            break;
                        case 'E':
                            components[componentsRow, column] = new GateInput();
                            break;
                        case 'A': case 'O': case 'N': case 'X':
                            components[componentsRow, column] =
                                new GateType(rawInputData[rawInputDataRow][column]);
                            break;
                        case 'S':
                            components[componentsRow, column] = new GateOutput();
                            break;
                    }
                }
            }

            // Connect conductor components
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                {
                    // PowerSupply components should not be connected at this point
                    if (components[i, j] is Conductor && !(components[i, j] is PowerSupply))
                    {
                        // Conexión nodo superior
                        if (i > 0 && components[i - 1, j] is Conductor)
                        {
                            (components[i, j] as Conductor).Connections
                                .Add(components[i - 1, j] as Conductor);
                        }

                        // Connect bottom node
                        if (i < (numRows - 1) && components[i + 1, j] is Conductor)
                        {
                            (components[i, j] as Conductor).Connections
                                .Add(components[i + 1, j] as Conductor);
                        }

                        // Connect left node
                        if (j > 0 && components[i, j - 1] is Conductor)
                        {
                            (components[i, j] as Conductor).Connections
                                .Add(components[i, j - 1] as Conductor);
                        }

                        // Connect right node
                        if (j < (numColumns - 1) && components[i, j + 1] is Conductor)
                        {
                            (components[i, j] as Conductor).Connections
                                .Add(components[i, j + 1] as Conductor);
                        }
                    }
                }
            }

            const short
                  GATE_WIDTH = 5
                , GATE_HEIGHT = 2;

            // Detect logic gates
            bool gateDetected = false;

            for (int i = 0; i <= (numRows - GATE_HEIGHT); ++i)
            {
                for (int j = 0; j <= (numColumns - GATE_WIDTH);
                    j += (gateDetected)? GATE_WIDTH : 1)
                {
                    // Verify if a component is GateCase
                    if (components[i, j] is GateCase) {

                        // Are we in the row that's just on top of the gate
                        if (
                            // Gate's first row
                               components[i, j + 1] is GateInput
                            && components[i, j + 2] is GateType
                            && components[i, j + 3] is GateInput
                            && components[i, j + 4] is GateCase

                            // Gate's second row
                            && components[i + 1, j] is GateCase
                            && components[i + 1, j + 1] is GateCase
                            && components[i + 1, j + 2] is GateOutput
                            && components[i + 1, j + 3] is GateCase
                            && components[i + 1, j + 4] is GateCase
                            )
                        {
                            // An input list is created for the new Gate
                            List<GateInput> input = new List<GateInput>() {
                                  (components[i, j + 1] as GateInput)
                                , (components[i, j + 3] as GateInput)
                            };

                            Gate gate;

                            switch ((components[i, j + 2] as GateType).GateTypeLetter)
                            {
                                case 'A':
                                    gate = new GateAnd(input);
                                    break;
                                case 'O':
                                    gate = new GateOr(input);
                                    break;
                                case 'N':
                                    gate = new GateNot(input);
                                    break;
                                default:
                                    gate = new GateXor(input);
                                    break;
                            }

                            // The gate is linked to its output
                            (components[i + 1, j + 2] as GateOutput).AssociatedGate = gate;
                            Gates.Add(gate);

                            gateDetected = true;
                        }

                        // Are we in the row that's just below the Gate?
                        else if (
                               components[i - 1, j] is GateCase
                            && components[i - 1, j + 1] is GateInput
                            && components[i - 1, j + 2] is GateType
                            && components[i - 1, j + 3] is GateInput
                            && components[i - 1, j + 4] is GateCase

                            // Gate's second row
                            && components[i, j + 1] is GateCase
                            && components[i, j + 2] is GateOutput
                            && components[i, j + 3] is GateCase
                            && components[i, j + 4] is GateCase
                            )
                        {
                            gateDetected = true;
                        }

                        // If the GateCase is not next to the Gate (from top or bottom)
                        // then the Gate is invalid.
                        else
                        {
                            throw new Exception("Invalid gate detected!");
                        }
                    }

                    // In case component is not a GateCase, just keep iterating.
                    else
                    {
                        gateDetected = false;
                    }
                }
            }
        }

        public override string ToString()
        {
            string outputString = "";

            outputString += "   ";

            for (int j = 0; j < components.GetLength(1); ++j)
            {
                outputString += components[0, j].ToString();
            }

            outputString += " \n";

            outputString += "  -";
            for (int j = 0; j < components.GetLength(1); ++j)
            {
                outputString += "-·-";
            }
            outputString += "-";
            outputString += "\n";
            
            for (int i = 1; i < components.GetLength(0); ++i)
            {

                outputString += "  |";

                for (int j = 0; j < components.GetLength(1); ++j)
                {
                    outputString += components[i, j].ToString();
                }

                outputString += "|\n";
            }
            
            outputString += "  -";
            for (int j = 0; j < components.GetLength(1); ++j)
            {
                outputString += "---";
            }
            outputString += "-";
            outputString += "\n";

            return outputString;
        }
    }
}

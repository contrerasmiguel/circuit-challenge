/*
 * Author: Miguel Contreras
 * 
 * 
 * 
 */

using System;

namespace Circuit
{
    class Program
    {
        private static short 
              MIN_ROWS = 4
            , MAX_ROWS = 60
            , MIN_COLUMNS = 6
            , MAX_COLUMNS = 100;
         
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                InputData inputData = new InputData(args[0], MIN_ROWS, MAX_ROWS, MIN_COLUMNS
                    , MAX_COLUMNS);

                try
                {
                    char[][] rawInputData = inputData.ParseFile();
                    Components components = new Components(rawInputData);

                    Console.WriteLine("\nCircuit\n=======\n\n"
                        + components);

                    Console.WriteLine("Number of logic gates: " 
                        + components.Gates.Count + "\n");
                  
                    foreach (Gate gate in components.Gates)
                    {
                        Console.WriteLine(gate);
                    }

                    Console.WriteLine("\n");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.Error.WriteLine("You must specify an input file.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Circuit
{
    class InputData
    {
        private string fileName;

        private short
              minRows
            , maxRows
            , minColumns
            , maxColumns;

        public InputData(string fileName, short minRows, short maxRows, short minColumns
            , short maxColumns)
        {
            this.fileName = fileName;

            this.minRows = minRows;
            this.maxRows = maxRows;
            this.minColumns = minColumns;
            this.maxColumns = maxColumns;
        }

        public char[][] ParseFile()
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);

            if (lines.Length == 0)
            {
                throw new Exception("Input file is empty!");
            }

            Match match = Regex.Match(lines[0], @"(\d+)\s+(\d+)");

            if (!match.Success)
            {
                throw new Exception("First line of the input file must have two space-separated integers!");
            }

            short numRows = short.Parse(match.Groups[1].Value)
                , numColumns = short.Parse(match.Groups[2].Value);

            if (numRows < minRows || numRows > maxRows)
            {
                throw new Exception("Number of rows must be between " + minRows + " and "
                    + maxRows + "!");
            }

            if (numColumns < minColumns || numColumns > maxColumns)
            {
                throw new Exception("Number of columns must be between " + minColumns + " and "
                    + maxColumns + "!");
            }

            if ((lines.Length - 1) < numRows)
            {
                throw new Exception("Matrix of " + numRows + " rows expected; found "
                    + " a matrix of " + (lines.Length - 1) + " rows!");
            }

            List<char[]> matrixLines = new List<char[]>();

            foreach (string l in lines.ToList().GetRange(1, (lines.Length - 1)))
            {
                if (l.Length < numColumns)
                {
                    throw new Exception("One of the lines has less than " + numColumns 
                        + " columns!");
                }
                else
                {
                    matrixLines.Add(l.ToUpper().ToArray());
                }
            }

            return matrixLines.ToArray();
        }
    }
}

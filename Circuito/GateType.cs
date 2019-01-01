namespace Circuit
{
    class GateType : Component
    {
        public char GateTypeLetter { get; }

        public GateType(char gateTypeLetter)
        {
            this.GateTypeLetter = gateTypeLetter;
        }

        public override string ToString()
        {
            return
                  (GateTypeLetter == 'A') ? "AND"
                : (GateTypeLetter == 'O') ? "OR░"
                : (GateTypeLetter == 'N') ? "NOT"
                :                           "XOR";
        }
    }
}

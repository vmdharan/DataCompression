namespace DataCompression
{
    class CFData
    {
        public char key { get; set; } // Character
        public int frequency { get; set; } // Frequency of occurrence
        public string bitstring { get; set; } // Bitstring

        public CFData()
        {
            key = '\0';
            frequency = 0;
            bitstring = "x";
        } 
    }
}

namespace DataCompression
{
    class CFList
    {
        public CFNode head { get; set; }
        public CFNode foot { get; set; }

        public CFList()
        {
            head = null;
            foot = null;
        }
    }
}

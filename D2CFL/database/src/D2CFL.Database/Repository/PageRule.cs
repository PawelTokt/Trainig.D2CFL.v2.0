namespace D2CFL.Database.Repository
{
    public class PageRule
    {
        public int Index { get; set; }

        public int Size { get; set; }

        public bool IsValid => Index >= 0 && Size > 0;
    }
}

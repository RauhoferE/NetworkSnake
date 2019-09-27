namespace Snake_V_0_3
{
    public class DirectionEventArgs
    {
        public DirectionEventArgs(IDirection e)
        {
            this.Direction = e;
        }

        public IDirection Direction
        {
            get;
            private set;
        }
    }
}
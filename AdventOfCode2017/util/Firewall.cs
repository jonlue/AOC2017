namespace AdventOfCode2017.util
{
    
    public class Firewall
    {
        private int Depth { get; }
        private int Range { get; }
        private int Position { get; set; }
        private int Direction { get; set; }

        public Firewall(int depth, int range)
        {
            Depth = depth;
            Range = range;
            Position = 1;
            Direction = 1;
        }
        
        public Firewall(Firewall f)
        {
            Depth = f.Depth;
            Range = f.Range;
            Position = f.Position;
            Direction = f.Direction;
        }

        public void Step()
        {
            Position += Direction;
            if (Position == Range || Position == 1)
            {
                Direction *= -1;
            }
        }

        public int Caught()
        {
            // Return Score if caught otherwise 0
            if (Position == 1)
            {
                return Depth * Range;
            }

            return 0;
        }
    }
}
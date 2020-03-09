namespace RobotCleaner.Models
{
    public sealed class Instruction
    {
        public Instruction(char direction, int steps)
        {
            this.Direction = direction;
            this.Steps = steps;
        }

        public char Direction { get; }
        public int Steps { get; }
    }
}

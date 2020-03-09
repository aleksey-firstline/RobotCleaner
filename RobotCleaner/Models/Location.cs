namespace RobotCleaner.Models
{
    public sealed class Location
    {
        public Location(int column, int row)
        {
            this.Column = column;
            this.Row = row;
        }

        public int Column { get; }
        public int Row { get; }

        public override int GetHashCode() => 
            (Column, Row).GetHashCode();

        public override bool Equals(object obj)
        {
            if (!(obj is Location location))
                return false;

            return Column == location.Column &&
                   Row == location.Row;
        }
    }
}

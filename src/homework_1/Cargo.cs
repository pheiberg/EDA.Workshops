namespace homework_1
{
    public class Cargo
    {
        public string Origin { get; }
        public string Destination { get; }
        public int Id { get; }
        
        public Cargo(int id, string origin, string destination)
        {
            Id = id;
            Origin = origin;
            Destination = destination;
        }
    }
}
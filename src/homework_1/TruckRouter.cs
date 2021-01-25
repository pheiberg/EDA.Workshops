namespace homework_1
{
    public class TruckRouter : IRouter
    {
        private readonly Location _port;
        private readonly Location _b;

        public TruckRouter(Location port, Location b)
        {
            _port = port;
            _b = b;
        }
        
        public Location Route(string goods)
        {
            return goods switch
            {
                null => null,
                "A" => _port,
                _ => _b
            };
        }
    }
}
using Newtonsoft.Json;

namespace homework_1
{
    public abstract class VehicleStatusChanged
    {
        public virtual string Event => GetType().Name.ToUpper();
        public int Time { get; init; }
        [JsonProperty(PropertyName = "transport_id")]
        public int TransportId { get; init; }
        public string Kind { get; init; }
        public string Location { get; init; }
        public string Destination { get; init; }
        public CargoInfo[] Cargo { get; init; }
    }

    public class CargoInfo
    {
        [JsonProperty(PropertyName = "cargo_id")]
        public int CargoId { get; init; }
        public string Destination { get; init; }
        public string Origin { get; init; }
    }

    public class Departed : VehicleStatusChanged
    {
        public override string Event => "DEPART";
    }
    
    public class Arrived : VehicleStatusChanged
    {
        public override string Event => "ARRIVE";
    }
}
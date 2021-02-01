using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;

namespace homework_1
{
    public class TransportTests
    {
        [Theory]
        [InlineData(5, "B")]
        [InlineData(5, "A")]
        [InlineData(5, "A", "B")]
        [InlineData(7, "A", "B", "B")]
        [InlineData(29, "A", "A", "B", "A", "B", "B", "A", "B")]
        [InlineData(29, "A", "A", "A", "A", "B", "B", "B", "B")]
        [InlineData(49, "B", "B", "B", "B", "A", "A", "A", "A")]
        public void Run_InlineData_TimeIsCorrect(int time, params string[] locations)
        {
            var ep = new EventPublisher();
            var sut = new Transport(ep);

            var result = sut.Execute(locations);
            
            Assert.Equal(time, result);
        }
        
        [Fact]
        public void Run_AB_EventsAreCorrect()
        {
            var expected = new[]
            {
                "{\"event\": \"DEPART\", \"time\": 0, \"transport_id\": 0, \"kind\": \"TRUCK\", \"location\": \"FACTORY\", \"destination\": \"PORT\", \"cargo\": [{\"cargo_id\": 0, \"destination\": \"A\", \"origin\": \"FACTORY\"}]}",
                "{\"event\": \"DEPART\", \"time\": 0, \"transport_id\": 1, \"kind\": \"TRUCK\", \"location\": \"FACTORY\", \"destination\": \"B\", \"cargo\": [{\"cargo_id\": 1, \"destination\": \"B\", \"origin\": \"FACTORY\"}]}",
                "{\"event\": \"ARRIVE\", \"time\": 1, \"transport_id\": 0, \"kind\": \"TRUCK\", \"location\": \"PORT\", \"cargo\": [{\"cargo_id\": 0, \"destination\": \"A\", \"origin\": \"FACTORY\"}]}",
                "{\"event\": \"DEPART\", \"time\": 1, \"transport_id\": 0, \"kind\": \"TRUCK\", \"location\": \"PORT\", \"destination\": \"FACTORY\"}",
                "{\"event\": \"DEPART\", \"time\": 1, \"transport_id\": 2, \"kind\": \"SHIP\", \"location\": \"PORT\", \"destination\": \"A\", \"cargo\": [{\"cargo_id\": 0, \"destination\": \"A\", \"origin\": \"FACTORY\"}]}",
                "{\"event\": \"ARRIVE\", \"time\": 2, \"transport_id\": 0, \"kind\": \"TRUCK\", \"location\": \"FACTORY\"}",
                "{\"event\": \"ARRIVE\", \"time\": 5, \"transport_id\": 1, \"kind\": \"TRUCK\", \"location\": \"B\", \"cargo\": [{\"cargo_id\": 1, \"destination\": \"B\", \"origin\": \"FACTORY\"}]}",
                "{\"event\": \"DEPART\", \"time\": 5, \"transport_id\": 1, \"kind\": \"TRUCK\", \"location\": \"B\", \"destination\": \"FACTORY\"}",
                "{\"event\": \"ARRIVE\", \"time\": 5, \"transport_id\": 2, \"kind\": \"SHIP\", \"location\": \"A\", \"cargo\": [{\"cargo_id\": 0, \"destination\": \"A\", \"origin\": \"FACTORY\"}]}",
                "{\"event\": \"DEPART\", \"time\": 5, \"transport_id\": 2, \"kind\": \"SHIP\", \"location\": \"A\", \"destination\": \"PORT\"}"
            }.Select(NormalizeJson).ToArray();
            
            var events = new List<string>();
            var eventPublisher = new EventPublisher();
            eventPublisher.Subscribe(e => events.Add(e));
            var sut = new Transport(eventPublisher);

            sut.Execute(new[]{"A", "B"});

            var results = events.Select(NormalizeJson).ToArray();

            Assert.Equal(expected, results);
        }

        private static VehicleStatusChanged Parse(string e)
        {
            if(e.Contains("\"DEPART\""))
                return JsonConvert.DeserializeObject<Departed>(e);
            return JsonConvert.DeserializeObject<Arrived>(e);
        }

        private static string NormalizeJson(string o)
        {
            return JsonConvert.SerializeObject(Parse(o));
        }
    }
}
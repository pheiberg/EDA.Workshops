using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace homework_1
{
    public class EventPublisher : IEventPublisher, IEventSource
    {
        private readonly IList<Action<string>> _subscribers;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new()
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
            NullValueHandling = NullValueHandling.Ignore
        };

        public EventPublisher(IEnumerable<Action<string>> subscribers = default)
        {
            _subscribers = subscribers?.ToList() ?? new List<Action<string>>();
        }
        
        public void Publish(object e)
        {
            var json = JsonConvert.SerializeObject(e, _jsonSerializerSettings);
            foreach (var subscriber in _subscribers)
            {
                subscriber(json);
            }
        }

        public void Subscribe(Action<string> subscriber)
        {
            _subscribers.Add(subscriber);
        }
    }

    public interface IEventSource
    {
        void Subscribe(Action<string> subscriber);
    }

    public interface IEventPublisher
    {
        void Publish(object e);
    }
}
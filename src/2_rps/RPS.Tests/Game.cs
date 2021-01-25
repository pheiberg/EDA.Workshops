using System;
using System.Collections.Generic;
using System.Text;

namespace RPS.Tests
{
    public static class Game
    {
        public static IEnumerable<IEvent> Handle(CreateGame command, GameState state)
        {
            var events = new List<IEvent>();
            
            events.Add(new GameCreated
            {
                GameId = command.GameId,
                Meta = new Dictionary<string, string>(),
                PlayerId = command.PlayerId,
            });
            
            events.Add(new GameStarted
            {
                GameId = command.GameId,
                Meta = new Dictionary<string, string>(),
                PlayerId = command.PlayerId,
            });
            return events;
        }

        public static IEnumerable<IEvent> Handle(JoinGame command, GameState state)
        {
            _events.Clear();
            return _events;
        }

        public static IEnumerable<IEvent> Handle(PlayGame command, GameState state)
        {
            _events.Add(new HandShown
            {
                GameId = command.GameId,
                Meta = new Dictionary<string, string>(),
            });
            return _events;
        }
    }
}

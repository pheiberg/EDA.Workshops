using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invariants.Tests
{
    public static class Game
    {
        public static IEnumerable<IEvent> Handle(JoinGame command, GameState state)
        {
            if (state.Player1 == command.PlayerId)
                yield break;
            
            yield return new GameStarted { GameId = command.GameId, PlayerId = command.PlayerId };
            yield return new RoundStarted { GameId = command.GameId, Round = 1 };
        }

        public static IEnumerable<IEvent> Handle(JoinGame command, IEnumerable<IEvent> events)
        {
            if(events.OfType<GameCreated>().Any(e => e.PlayerId == command.PlayerId))
                yield break;
            
            yield return new GameStarted { GameId = command.GameId, PlayerId = command.PlayerId };
            yield return new RoundStarted { GameId = command.GameId, Round = 1 };
        }

    }
}

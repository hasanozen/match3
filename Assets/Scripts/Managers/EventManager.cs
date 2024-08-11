using System;
using System.Collections.Generic;
using Enums;
using Utils;

namespace Managers
{
    public class EventManager : PersistentSingleton<EventManager>
    {
        private readonly Dictionary<GameEvents, HashSet<Action>> _handlers = new();
        private readonly Dictionary<GameEvents, HashSet<Action<object>>> _handlersSingleArg = new();
        private readonly Dictionary<GameEvents, HashSet<Action<object, object>>> _handlersTwoArgs = new();
        private readonly Dictionary<GameEvents, HashSet<Action<object, object, object>>> _handlersThreeArgs = new();

        public void AddHandler(GameEvents gameEvent, Action handler)
        {
            if (!_handlers.ContainsKey(gameEvent)) 
                _handlers[gameEvent] = new HashSet<Action>();

            _handlers[gameEvent].Add(handler);
        }

        public void AddHandler<T>(GameEvents gameEvent, Action<T> handler)
        {
            if (!_handlersSingleArg.ContainsKey(gameEvent))
                _handlersSingleArg[gameEvent] = new HashSet<Action<object>>();

            _handlersSingleArg[gameEvent].Add(arg => handler((T) arg));
        }

        public void AddHandler<T, T1>(GameEvents gameEvent, Action<T, T1> handler)
        {
            if (!_handlersTwoArgs.ContainsKey(gameEvent))
                _handlersTwoArgs[gameEvent] = new HashSet<Action<object, object>>();

            _handlersTwoArgs[gameEvent].Add((arg1, arg2) => handler((T) arg1, (T1) arg2));
        }

        public void AddHandler<T, T1, T2>(GameEvents gameEvent, Action<T, T1, T2> handler)
        {
            if (!_handlersThreeArgs.ContainsKey(gameEvent))
                _handlersThreeArgs[gameEvent] = new HashSet<Action<object, object, object>>();

            _handlersThreeArgs[gameEvent].Add((arg1, arg2, arg3) => handler((T) arg1, (T1) arg2, (T2) arg3));
        }

        public void RemoveHandler(GameEvents gameEvent, Action handler)
        {
            if (_handlers.ContainsKey(gameEvent))
            {
                if (_handlers[gameEvent].Count == 1)
                    _handlers[gameEvent] = new HashSet<Action>();

                _handlers[gameEvent].Remove(handler);
            }
        }

        public void RemoveHandler<T>(GameEvents gameEvent, Action<T> handler)
        {
            if (_handlersSingleArg.ContainsKey(gameEvent))
            {
                if (_handlersSingleArg[gameEvent].Count == 1)
                    _handlersSingleArg[gameEvent] = new HashSet<Action<object>>();

                _handlersSingleArg[gameEvent].Remove(arg => handler((T) arg));
            }
        }

        public void RemoveHandler<T, T1>(GameEvents gameEvent, Action<T, T1> handler)
        {
            if (_handlersTwoArgs.ContainsKey(gameEvent))
            {
                if (_handlersTwoArgs[gameEvent].Count == 1)
                    _handlersTwoArgs[gameEvent] = new HashSet<Action<object, object>>();

                _handlersTwoArgs[gameEvent].Remove((arg1, arg2) => handler((T) arg1, (T1) arg2));
            }
        }

        public void RemoveHandler<T, T1, T2>(GameEvents gameEvent, Action<T, T1, T2> handler)
        {
            if (_handlersThreeArgs.ContainsKey(gameEvent))
            {
                if (_handlersThreeArgs[gameEvent].Count == 1)
                    _handlersThreeArgs[gameEvent] = new HashSet<Action<object, object, object>>();

                _handlersThreeArgs[gameEvent].Remove((arg1, arg2, arg3) => handler((T) arg1, (T1) arg2, (T2) arg3));
            }
        }

        public void Broadcast(GameEvents gameEvent)
        {
            if (_handlers.TryGetValue(gameEvent, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler();
                }
            }
        }

        public void Broadcast<T>(GameEvents gameEvent, T arg)
        {
            if (_handlersSingleArg.TryGetValue(gameEvent, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler(arg);
                }
            }
        }

        public void Broadcast<T, T1>(GameEvents gameEvent, T arg1, T1 arg2)
        {
            if (_handlersTwoArgs.TryGetValue(gameEvent, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler(arg1, arg2);
                }
            }
        }

        public void Broadcast<T, T1, T2>(GameEvents gameEvent, T arg1, T1 arg2, T2 arg3)
        {
            if (_handlersThreeArgs.TryGetValue(gameEvent, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler(arg1, arg2, arg3);
                }
            }
        }
    }
}
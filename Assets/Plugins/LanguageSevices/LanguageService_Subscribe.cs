using System;
using System.Collections;
using System.Collections.Generic;


namespace LanguageSevices
{
    public static partial class LanguageService
    {
        private static List<Action> _observers = new List<Action>();
        public static IDisposable Subscribe(Action action)
        {
            if (!_observers.Contains(action))
                _observers.Add(action);
            action.Invoke();
            return new Unsubscriber<Action>(_observers, action);
        }

        private static void UpdateLStrings()
        {
            var observersCopy = _observers;

            foreach (var observer in observersCopy)
            {
                if (observer != null)
                    observer.Invoke();
            }
        }
    }

    public class Unsubscriber<T> : IDisposable
    {
        private IList<T> _observers;
        private T _observer;

        public Unsubscriber
        (IList<T> observers, T observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
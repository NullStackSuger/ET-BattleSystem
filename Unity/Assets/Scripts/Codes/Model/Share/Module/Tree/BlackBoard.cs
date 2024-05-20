using System;
using System.Collections.Generic;

namespace ET
{
    public class BlackBoard
    {
        private Dictionary<string, object> data = new();
        private Dictionary<string, List<Action<string, object, object>>> observers = new();
        
        public object this[string key]
        {
            get
            {
                return Get<object>(key);
            }
            set
            {
                Set(key, value);
            }
        }

        public object Get(string key)
        {
            return this.data[key];
        }
        public T Get<T>(string key)
        {
            if (Contains(key)) return (T)this.data[key];
            return default;
        }

        public void Set(string key, object newValue)
        {
            object oldValue = Get(key);
            if (oldValue == newValue) return;

            this.data[key] = newValue;
            
            TriggerObserver(key, oldValue, newValue);
        }

        public bool Contains(string key)
        {
            return this.data.ContainsKey(key);
        }

        public void Clear()
        {
            data.Clear();
            this.observers.Clear();
        }

        public void AddObserver(string key, Action<string, object, object> observer)
        {
            if (!this.observers.ContainsKey(key))
                this.observers.Add(key, new List<Action<string, object, object>>());
            this.observers[key].Add(observer);
        }

        public void RemoveObserver(string key, Action<string, object, object> observer)
        {
            if (!this.observers.ContainsKey(key)) return;
            if(!this.observers[key].Contains(observer)) return;
            this.observers[key].Remove(observer);
        }

        private void TriggerObserver(string key, object oldValue, object newValue)
        {
            if (!this.observers.ContainsKey(key)) return;
            for(int i = this.observers[key].Count - 1; i >= 0; --i)
            {
                this.observers[key][i]?.Invoke(key, oldValue, newValue);
            }
        }
    }
}
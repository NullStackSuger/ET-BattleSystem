using System.Collections.Generic;

namespace ET
{
    public class BlackBoard
    {
        private Dictionary<string, object> data = new();
        
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

        public T Get<T>(string key)
        {
            if (Contains(key)) return (T)this.data[key];
            return default;
        }

        public void Set(string key, object value)
        {
            if (Contains(key))
            {
                object old = Get<object>(key);

                // OldValue != null, NewValue == null
                if (value == null)
                {
                    this.data.Remove(key);
                    EventSystem.Instance.Publish(Root.Instance.Scene.DomainScene(), new EventType.BlackboardValueChanged()
                    {
                        BlackBoard = this, Key = key, OldValue = old, NewValue = null
                    });
                }
                // OldValue != null, NewValue != null
                else
                {
                    if (old == value) return;

                    this.data[key] = value;
                    EventSystem.Instance.Publish(Root.Instance.Scene.DomainScene(), new EventType.BlackboardValueChanged()
                    {
                        BlackBoard = this, Key = key, OldValue = old, NewValue = value
                    });
                }
            }
            else
            {
                // OldValue == null, NewValue == null
                if (value == null) return;
                
                // OldValue == null, NewValue != null
                data[key] = value;
                EventSystem.Instance.Publish(Root.Instance.Scene.DomainScene(), new EventType.BlackboardValueChanged()
                {
                    BlackBoard = this, Key = key, OldValue = null, NewValue = value
                });
            }
        }

        public bool Contains(string key)
        {
            return this.data.ContainsKey(key);
        }

        public void Clear()
        {
            data.Clear();
        }
    }
}
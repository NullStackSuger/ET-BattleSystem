using BulletSharp;

namespace ET
{

    public abstract class SingletonContactResultCallback<T> : ContactResultCallback where T : ContactResultCallback, new()
    {
        [StaticField]
        private static T instance;
        
        public static T Instance
        {
            get
            {
                instance ??= new T();
                return instance;
            }
        }
    }
}
namespace ET
{
    namespace EventType
    {
        public struct SceneChangeStart
        {
        }
        
        public struct SceneChangeFinish
        {
        }
        
        public struct AfterCreateClientScene
        {
        }
        
        public struct AfterCreateCurrentScene
        {
        }

        public struct AppStartInitFinish
        {
        }

        public struct LoginFinish
        {
        }

        public struct EnterMapFinish
        {
        }

        public struct AfterUnitCreate
        {
            public Unit Unit;
        }

        public struct CastCreat
        {
            public long CastId;
        }

        public struct CastTick
        {
            
        }

        public struct CastRemove
        {
            
        }

        public struct BuffCreat
        {
            
        }

        public struct BuffTick
        {
            
        }

        public struct BuffRemove
        {
            
        }
    }
}
using System;

namespace ET
{
    public static class TimeHelper
    {
        public const long OneDay = 86400000;
        public const long Hour = 3600000;
        public const long Minute = 60000;
        
        /// <summary>
        /// 客户端时间
        /// </summary>
        /// <returns></returns>
        public static long ClientNow()
        {
            return TimeInfo.Instance.ClientNow();
        }

        public static long ClientNowSeconds()
        {
            return ClientNow() / 1000;
        }

        public static DateTime DateTimeNow()
        {
            return DateTime.Now;
        }

        public static long ServerNow()
        {
            return TimeInfo.Instance.ServerNow();
        }

        public static long ClientFrameTime()
        {
            return TimeInfo.Instance.ClientFrameTime();
        }
        
        public static long ServerFrameTime()
        {
            return TimeInfo.Instance.ServerFrameTime();
        }

        public static long DeltaTime()
        {
            return TimeInfo.Instance.DeltaTIme;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">时长</param>
        /// <param name="frameRate">每秒多少帧</param>
        /// <returns></returns>
        public static long ToFrame(long time, uint frameRate)
        {
            return time * frameRate;
        }
    }
}
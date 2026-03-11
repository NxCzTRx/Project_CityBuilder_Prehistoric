namespace _Scripts.Core.DayCycle
{
    public struct GameTime
    {
        public int Hour {get;}
        public int Minute { get; }
    
        public GameTime(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }
    }
}

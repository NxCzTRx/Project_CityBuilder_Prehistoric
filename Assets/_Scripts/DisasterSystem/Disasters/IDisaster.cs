namespace _Scripts.DisasterSystem.Disasters
{
    public interface IDisaster
    {
        void OnStart();
        void OnEnd();
        float Duration { get; }
    }
}


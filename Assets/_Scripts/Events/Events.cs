using _Scripts.BuildSystem;

namespace _Scripts.Events
{
    public interface IEvent { }

    public struct OnBuildingSelected : IEvent
    {
        public BuildingSO BuildingSo { get; }
        
        public OnBuildingSelected(BuildingSO buildingSo)
        {
            BuildingSo = buildingSo;
        }
    }
}
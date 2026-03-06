using UnityEngine;

namespace _Scripts.BuildSystem.Building
{
    [RequireComponent(typeof(BuildingView))]
    public class BuildingEntity : MonoBehaviour
    {
        private BuildingController _buildingController;

        public void Init(BuildingSO buildingSO)
        {
            var model = new BuildingModel(buildingSO);
            var view = GetComponent<BuildingView>();

            _buildingController = new BuildingController(model, view);
        }
    }
}

using _Scripts.Core;
using UnityEngine;

namespace _Scripts.BuildSystem.Building
{
    [RequireComponent(typeof(BuildingView))]
    public class BuildingEntity : MonoBehaviour
    {
        private BuildingController _buildingController;

        public void Init(BuildingSO buildingSO, ObjectResolver objectResolver)
        {
            var model = new BuildingModel(buildingSO);
            var view = GetComponent<BuildingView>();

            _buildingController = new BuildingController(model, view, objectResolver.Resolve<PawnRegistry>());
        }
    }
}

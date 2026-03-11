using _Scripts.Core;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.BuildSystem.Building
{
    [RequireComponent(typeof(BuildingView))]
    public class BuildingEntity : MonoBehaviour
    {
        private BuildingController _buildingController;

        public void Init(BuildingSO buildingSO, Cell workCell,ObjectResolver objectResolver)
        {
            var model = new BuildingModel(buildingSO, workCell);
            var view = GetComponent<BuildingView>();

            _buildingController = new BuildingController(model, view, objectResolver);
        }
    }
}

using _Scripts.BuildSystem.Building.WorkPlace;
using _Scripts.Core;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.BuildSystem.Building
{
    [RequireComponent(typeof(WorkPlaceView))]
    public class WorkPlaceEntity : MonoBehaviour
    {
        private WorkPlaceController _workPlaceController;

        public void Init(WorkPlaceSO workPlaceSO, Cell workCell,ObjectResolver objectResolver)
        {
            var model = new WorkPlaceModel(workPlaceSO, workCell);
            var view = GetComponent<WorkPlaceView>();

            _workPlaceController = new WorkPlaceController(model, view, objectResolver);
        }
    }
}

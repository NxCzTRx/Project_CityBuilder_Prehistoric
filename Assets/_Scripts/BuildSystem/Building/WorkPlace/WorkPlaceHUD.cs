using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Roles;
using _Scripts.BuildSystem.Building.WorkPlace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BuildSystem.Building
{
    public class WorkPlaceHUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text buildingNameTMP;
        [SerializeField] private Button addWorkerButton;
        [SerializeField] private Button removeWorkerButton;
        [SerializeField] private TMP_Text workerNumberTMP;
        
        private WorkPlaceController _controller;
        private PawnRegistry _pawnRegistry;

        public void Init(WorkPlaceController controller, PawnRegistry pawnRegistry)
        {
            _controller = controller;
            _pawnRegistry = pawnRegistry;

            buildingNameTMP.text = _controller.Model.WorkPlaceSO.BuildingName;
            SetWorkerNumberTMP();
        }

        public void AddWorker()
        {
            if(!_pawnRegistry.HasAvailablePawn || !_controller.HasSpace ) return;

            var pawn = _pawnRegistry.GetAvailablePawn();
            _controller.AssignWorker(pawn);
            SetWorkerNumberTMP();
        }

        public void RemoveWorker()
        {
            _controller.RemoveWorker();
            SetWorkerNumberTMP();
        }

        private void SetWorkerNumberTMP()
        {
            workerNumberTMP.text = _controller.Model.PawnWorkers.Count.ToString() + "/"
                + _controller.Model.WorkPlaceSO.MaxWorkers.ToString();
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BuildSystem.Building
{
    public class BuildingHUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text buildingNameTMP;
        [SerializeField] private Button addWorkerButton;
        [SerializeField] private Button removeWorkerButton;
        [SerializeField] private TMP_Text workerNumberTMP;
        
        private BuildingController _controller;
        private PawnRegistry _pawnRegistry;

        public void Init(BuildingController controller, PawnRegistry pawnRegistry)
        {
            _controller = controller;
            _pawnRegistry = pawnRegistry;

            buildingNameTMP.text = _controller.Model.BuildingSO.BuildingName;
            workerNumberTMP.text = "0";
        }

        public void AddWorker()
        {
            if(!_pawnRegistry.HasAvailablePawn || !_controller.HasSpace ) return;

            var pawn = _pawnRegistry.GetAvailablePawn();
            _controller.AssignWorker(pawn);
            workerNumberTMP.text = _controller.Model.PawnWorkers.Count.ToString();
        }

        public void RemoveWorker()
        {
            _controller.RemoveWorker();
            workerNumberTMP.text = _controller.Model.PawnWorkers.Count.ToString();
        }
    }
}

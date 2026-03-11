using _Scripts.AI.Entities.Pawn;
using UnityEngine;

namespace _Scripts.BuildSystem.Building.WorkPlace
{
    public class WorkPlaceView : MonoBehaviour, ISelectable
    {
        [SerializeField] WorkPlaceHUD workPlaceHUD;
        
        public void Init(WorkPlaceController controller, PawnRegistry pawnRegistry)
        {
            workPlaceHUD.Init(controller, pawnRegistry);
        }

        public void Select()
        {
            workPlaceHUD.gameObject.SetActive(true);
        }

        public void Unselect()
        {
            workPlaceHUD.gameObject.SetActive(false);
        }
    }
}

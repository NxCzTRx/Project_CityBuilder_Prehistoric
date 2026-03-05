using _Scripts.AI.Entities.Pawn.UI;
using _Scripts.AI.Pathfinding;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnView : MonoBehaviour, ISelectable
    {
        [SerializeField] private PawnHUD pawnHud;
        
        public Vector3 CurrentPosition => transform.position;

        public void Init(PawnModel model)
        {
            if (pawnHud == null)
            {
                Debug.LogError("PawnHUD not assigned to inspector", this);
                return;
            }
            
            pawnHud.Init(model);
        }
        
        public void MoveTowards(Vector3 target, float speed)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, target, speed * Time.deltaTime);
        }

        public bool HasReached(Vector3 target) => (CurrentPosition - target).sqrMagnitude < 0.01f;
        public void Select()
        {
            pawnHud.gameObject.SetActive(true);
        }

        public void Unselect()
        {
            pawnHud.gameObject.SetActive(false);
        }
    }
}

using _Scripts.AI.Pathfinding;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnView : MonoBehaviour
    {
        public Vector3 CurrentPosition => transform.position;
        
        public void MoveTowards(Vector3 target, float speed)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, target, speed * Time.deltaTime);
        }

        public bool HasReached(Vector3 target) => (CurrentPosition - target).sqrMagnitude < 0.01f;
    }
}

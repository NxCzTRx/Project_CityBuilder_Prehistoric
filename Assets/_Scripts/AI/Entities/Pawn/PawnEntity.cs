using System;
using _Scripts.AI.Entities.Pawn.UI;
using _Scripts.AI.FSM;
using _Scripts.AI.FSM.States;
using _Scripts.Core;
using _Scripts.Core.UpdateManagement;
using _Scripts.Grid;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnEntity : MonoBehaviour, IUpdateObserver
    {
        private GridManager _gridManager; 
        
        public PawnController PawnController { get; private set; }

        public void Init(ObjectResolver objectResolver, PawnSpawner mySpawner)
        {
            _gridManager = objectResolver.Resolve<GridManager>();
            
            var model = new PawnModel(_gridManager, transform.position);
            var view = GetComponent<PawnView>();

            PawnController = new PawnController(model, view, this, mySpawner);
            PawnController.ChangeState(new PawnMoveTo(PawnController, _gridManager.GetCell(new Vector2Int(10, 0))));
            
            UpdateManager.RegisterObserver(this);
        }
    
        public void ObservedUpdate() => PawnController.Tick();

        private void OnDestroy()
        {
            UpdateManager.UnregisterObserver(this);
        }
    }
}

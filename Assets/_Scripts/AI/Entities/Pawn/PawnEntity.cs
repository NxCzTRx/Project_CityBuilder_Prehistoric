using System;
using _Scripts.AI.Entities.Pawn.UI;
using _Scripts.AI.FSM;
using _Scripts.AI.FSM.States;
using _Scripts.Core;
using _Scripts.Core.UpdateManagement;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnEntity : MonoBehaviour, IUpdateObserver
    {
        private GridManager _gridManager; 
        private PawnController _pawnController;

        public void Init(ObjectResolver objectResolver)
        {
            _gridManager = objectResolver.Resolve<GridManager>();
            
            var model = new PawnModel(_gridManager, transform.position);
            var view = GetComponent<PawnView>();

            _pawnController = new PawnController(model, view);
            _pawnController.ChangeState(new PawnMoveTo(_pawnController, _gridManager.GetCell(new Vector2Int(10, 0))));
            
            UpdateManager.RegisterObserver(this);
        }
    
        public void ObservedUpdate() => _pawnController.Tick();
    }
}

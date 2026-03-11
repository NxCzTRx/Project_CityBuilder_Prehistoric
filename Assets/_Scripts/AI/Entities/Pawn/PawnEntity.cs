using System;
using _Scripts.AI.Entities.Pawn.UI;
using _Scripts.AI.FSM;
using _Scripts.AI.FSM.States;
using _Scripts.BuildSystem;
using _Scripts.Core;
using _Scripts.Core.UpdateManagement;
using _Scripts.Grid;
using _Scripts.ResourcesSystem.Resources.ResourceTypes;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnEntity : MonoBehaviour, IUpdateObserver
    {
        [SerializeField] private ResourceTypeSO food;
        
        private GridManager _gridManager; 
        
        public PawnController PawnController { get; private set; }

        public void Init(ObjectResolver objectResolver, PawnSpawner mySpawner)
        {
            _gridManager = objectResolver.Resolve<GridManager>();
            
            var model = new PawnModel(objectResolver.Resolve<GridManager>(), transform.position, food);
            var view = GetComponent<PawnView>();

            PawnController = new PawnController(model, view, this, mySpawner, objectResolver);
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

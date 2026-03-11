using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.AI.FSM.States;
using _Scripts.Core.DayCycle;

namespace _Scripts.AI.Entities.Pawn.Scheduling
{
    public class PawnScheduler : IDisposable
    {
        private readonly GameCycleManager _gameCycleManager;
        private readonly PawnRegistry _pawnRegistry;
        
        public PawnScheduler(GameCycleManager gameCycleManager, PawnRegistry pawnRegistry)
        {
            _gameCycleManager = gameCycleManager;
            _pawnRegistry = pawnRegistry;

            gameCycleManager.OnHourChanged += HandleHourChange;
        }

        public void EvaluatePawn(PawnController pawn)
        {
            if (pawn.Model.IsHandlingUrgentState) return;
            
            var hour = _gameCycleManager.GetTime().Hour;
    
            bool isWorkTime = (hour >= 8 && hour < 14) || (hour >= 18 && hour < 22);
            bool isSleepTime = hour >= 22 || hour < 6;

            if (isWorkTime && pawn.Model.WorkPlaceController != null)
                pawn.ChangeState(new PawnMoveToWorkState(pawn, pawn.Model.WorkPlaceController.Model.WorkCell));
            else if (isSleepTime && pawn.Model.HouseController != null)
                pawn.ChangeState(new PawnMoveToHouse(pawn, pawn.Model.HouseController.Model.EntranceCell));
            else
                pawn.ChangeState(new PawnIdleState(pawn));
        }

        private void HandleHourChange(int hour)
        {
            HandleHourChange(hour, _pawnRegistry.GetAllPawns());
        }

        private void HandleHourChange(int hour, IEnumerable<PawnController> pawns)
        {
            switch (hour)
            {
                case 8:
                case 18:
                    foreach (var pawn in pawns.Where(p =>
                                 p.Model.WorkPlaceController != null && !p.Model.IsHandlingUrgentState))
                        pawn.ChangeState(new PawnMoveToWorkState(pawn, pawn.Model.WorkPlaceController.Model.WorkCell));
                    break;

                case 6:
                case 14:
                case 21:
                    foreach (var pawn in pawns)
                        pawn.ChangeState(new PawnIdleState(pawn));
                    break;
                case 22:
                    foreach (var pawn in pawns.Where(p =>
                                 p.Model.HouseController != null && !p.Model.IsHandlingUrgentState))
                        pawn.ChangeState(new PawnMoveToHouse(pawn, pawn.Model.HouseController.Model.EntranceCell));
                    break;
            }
        }

        public void Dispose()
        {
            _gameCycleManager.OnHourChanged -= HandleHourChange;
        }
    }
}

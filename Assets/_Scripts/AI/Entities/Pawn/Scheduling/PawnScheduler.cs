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
            HandleHourChange(_gameCycleManager.GetTime().Hour, new[] { pawn });
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
                    foreach (var pawn in pawns.Where(p => p.Model.BuildingController != null))
                        pawn.ChangeState(new PawnMoveToWorkState(pawn, pawn.Model.BuildingController.Model.WorkCell));
                    break;

                case 6:
                case 14:
                case 21:
                    foreach (var pawn in pawns)
                        pawn.ChangeState(new PawnIdleState(pawn));
                    break;
            }
        }

        public void Dispose()
        {
            _gameCycleManager.OnHourChanged -= HandleHourChange;
        }
    }
}

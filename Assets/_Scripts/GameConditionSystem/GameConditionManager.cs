using System;
using _Scripts.AI.Entities.Pawn;
using _Scripts.Core;
using _Scripts.Events;
using _Scripts.NotificationSystem;
using _Scripts.TechTreeSystem;

public class GameConditionManager : IDisposable
{
    private readonly TechTreeManager _techTreeManager;
    private readonly PawnRegistry _pawnRegistry;
    private readonly NotificationManager _notificationManager;

    public GameConditionManager(ObjectResolver objectResolver)
    {
        _techTreeManager = objectResolver.Resolve<TechTreeManager>();
        _pawnRegistry = objectResolver.Resolve<PawnRegistry>();
        _notificationManager = objectResolver.Resolve<NotificationManager>();

        EventBus<OnNodeUnlocked>.Subscribe(CheckVictory);
        EventBus<OnPawnDied>.Subscribe(CheckDefeat);
    }

    private void CheckVictory(OnNodeUnlocked _)
    {
        if (!_techTreeManager.AllUnlocked()) return;
        _notificationManager.Notify("Victoria — has dominado todas las tecnologías", 30f);
    }

    private void CheckDefeat(OnPawnDied _)
    {
        if (_pawnRegistry.GetAllPawns().Count > 0) return;
        _notificationManager.Notify("Derrota — tu clan ha desaparecido", 30f);
    }

    public void Dispose()
    {
        EventBus<OnNodeUnlocked>.Unsubscribe(CheckVictory);
        EventBus<OnPawnDied>.Unsubscribe(CheckDefeat);
    }
}

using System.Collections.Generic;
using System.Linq;
using _Scripts.Core;
using _Scripts.Events;
using _Scripts.NotificationSystem;
using _Scripts.ResourcesSystem;
using _Scripts.ResourcesSystem.Resources;
using _Scripts.TechTreeSystem.TechEra;
using _Scripts.TechTreeSystem.TechNode;

namespace _Scripts.TechTreeSystem
{
    public class TechTreeManager
    {
        private ObjectResolver _resolver;
        private readonly HashSet<TechNodeSO> _unlocked = new();
        public TechEraSo[] Eras {get; }
        private int _currentEraIndex;

        private GameResourcesManager _gameResourcesManager;
        private NotificationManager _notificationManager;

        public TechTreeManager(TechEraSo[] eras)
        {
            Eras = eras;
        }

        public void Init(ObjectResolver objectResolver)
        {
            _resolver = objectResolver;
            
            _gameResourcesManager = _resolver.Resolve<GameResourcesManager>();
            _notificationManager = _resolver.Resolve<NotificationManager>();
        }

        public bool IsUnlocked(TechNodeSO techNode) => 
            _unlocked.Contains(techNode);

        public bool CanUnlock(TechNodeSO techNode)
        {
            if (IsUnlocked(techNode)) return false;

            if (!_gameResourcesManager.CanAfford(
                    new ResourceStock(techNode.KnowledgeResourceSo, techNode.TechCost)))
            {
                _notificationManager.Notify($"Not enough knowledge for {techNode.NodeName}.", 3f);
                return false;
            }
    
            return Eras[_currentEraIndex].TechNodes.Contains(techNode);
        }

        public bool TryUnlock(TechNodeSO techNode)
        {
            if (!CanUnlock(techNode)) return false;

            _unlocked.Add(techNode);
            _gameResourcesManager.RemoveResources(new ResourceStock(techNode.KnowledgeResourceSo, techNode.TechCost));
            _notificationManager.Notify(
                $"{techNode.NodeName} unlocked: {techNode.EffectDescription}", 7f);

            foreach (var effect in techNode.Effects)
                effect.Apply(_resolver);

            EventBus<OnNodeUnlocked>.Publish(new OnNodeUnlocked(techNode));
            CheckEraCompleted();
            return true;
        }

        private void CheckEraCompleted()
        {
            var eraCompleted = Eras[_currentEraIndex].TechNodes.All(n => _unlocked.Contains(n));
            if (!eraCompleted) return;

            if (_currentEraIndex < Eras.Length - 1)
                _currentEraIndex++;
            
            EventBus<OnEraCompleted>.Publish(new OnEraCompleted(Eras[_currentEraIndex]));
        }
        
        public bool IsInCurrentEra(TechNodeSO node) => 
            Eras[_currentEraIndex].TechNodes.Contains(node);
        
        public bool AllUnlocked() => 
            Eras.SelectMany(e => e.TechNodes).All(n => _unlocked.Contains(n));
    }
}

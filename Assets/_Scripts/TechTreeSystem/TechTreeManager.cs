using System.Collections.Generic;
using System.Linq;
using _Scripts.Events;
using _Scripts.ResourcesSystem;
using _Scripts.ResourcesSystem.Resources;
using _Scripts.TechTreeSystem.TechEra;
using _Scripts.TechTreeSystem.TechNode;

namespace _Scripts.TechTreeSystem
{
    public class TechTreeManager
    {
        private readonly HashSet<TechNodeSO> _unlocked = new();
        public TechEraSo[] Eras {get; }
        private int _currentEraIndex;

        private GameResourcesManager _gameResourcesManager;

        public TechTreeManager(TechEraSo[] eras, GameResourcesManager gameResourcesManager)
        {
            Eras = eras;
            _gameResourcesManager = gameResourcesManager;
        }

        public bool IsUnlocked(TechNodeSO techNode) => 
            _unlocked.Contains(techNode);

        public bool CanUnlock(TechNodeSO techNode)
        {
            if (IsUnlocked(techNode)) return false;
            if (!_gameResourcesManager.CanAfford(
                    new ResourceStock(techNode.KnowledgeResourceSo, techNode.TechCost))) 
                return false;
    
            return Eras[_currentEraIndex].TechNodes.Contains(techNode);
        }

        public bool TryUnlock(TechNodeSO techNode)
        {
            if (!CanUnlock(techNode)) return false;

            _unlocked.Add(techNode);
            _gameResourcesManager.RemoveResources(new ResourceStock(techNode.KnowledgeResourceSo, techNode.TechCost));

            //foreach (var effect in techNode.Effects)
                //effect.Apply();

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
    }
}

using System.Collections.Generic;
using _Scripts.ResourcesSystem.Resources.ResourceTypes;
using UnityEngine;

namespace _Scripts.ResourcesSystem.UI
{
    public class PlayerResourcesUI : MonoBehaviour
    {
        [SerializeField] private ResourceRowUI _resourceRowUIPrefabs;
    
        private readonly Dictionary<ResourceTypeSO, ResourceRowUI> _rows = new();
        private GameResourcesManager _gameResourcesManager;
        private bool _alreadyInitialized;

        private void OnEnable()
        {
            if (_alreadyInitialized)
                _gameResourcesManager.OnResourceAmountChanged += UpdateResourceAmount;
        }

        private void OnDisable()
        {
            if (_alreadyInitialized)
                _gameResourcesManager.OnResourceAmountChanged -= UpdateResourceAmount;
        }

        public void Init(GameResourcesManager gameResourcesManager)
        {
            _gameResourcesManager = gameResourcesManager;

            foreach (var resource in gameResourcesManager.GetAllResources())
            {
                var row = Instantiate(_resourceRowUIPrefabs, transform);
                row.SetUp(resource);
                _rows[resource.ResourceTypeSO] = row;
            }

            _alreadyInitialized = true;
            _gameResourcesManager.OnResourceAmountChanged += UpdateResourceAmount;
        }

        private void UpdateResourceAmount(ResourceTypeSO resourceTypeSO, float newAmount)
        {
            if (_rows.TryGetValue(resourceTypeSO, out var row))
                row.UpdateAmount(newAmount);
        }
    }
}
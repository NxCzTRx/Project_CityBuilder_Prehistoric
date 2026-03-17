using System.Collections.Generic;
using _Scripts.Events;
using _Scripts.ResourcesSystem;
using _Scripts.ResourcesSystem.Resources.ResourceTypes;
using UnityEngine;

namespace _Scripts.UI.Gameplay
{
    public class PlayerResourcesUI : MonoBehaviour
    {
        [SerializeField]
        private ResourceDataUI resourceDataUIPrefabs;
        
        private readonly Dictionary<ResourceTypeSO, ResourceDataUI> _resources = new();

        private void OnEnable()
        {
            EventBus<OnResourceAmountChanged>.Subscribe(UpdateResourceAmount);
        }

        private void OnDisable()
        {
            EventBus<OnResourceAmountChanged>.Unsubscribe(UpdateResourceAmount);
        }

        public void Init(GameResourcesManager gameResourcesManager)
        {
            foreach (var resource in gameResourcesManager.GetAllResources())
            {
                var row = Instantiate(resourceDataUIPrefabs, transform);
                row.SetUp(resource);
                _resources[resource.ResourceTypeSO] = row;
            }
        }

        private void UpdateResourceAmount(OnResourceAmountChanged ev)
        {
            if (_resources.TryGetValue(ev.ResourceTypeSO, out var resource))
                resource.UpdateAmount(ev.Amount);
        }
    }
}
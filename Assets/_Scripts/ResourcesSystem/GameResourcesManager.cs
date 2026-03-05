using System;
using System.Collections.Generic;
using _Scripts.ResourcesSystem.Resources;
using _Scripts.ResourcesSystem.Resources.ResourceTypes;

namespace _Scripts.ResourcesSystem
{
    public class GameResourcesManager
    {
        private readonly Dictionary<ResourceTypeSO, int> _resourcesAmount = new();
        
        public event Action<ResourceTypeSO, int> OnResourceAmountChanged;

        public GameResourcesManager(params ResourceStock[] resources)
        {
            foreach (var resource in resources)
            {
                if (_resourcesAmount.ContainsKey(resource.ResourceTypeSO))
                    throw new ArgumentException(
                        $"Resource '{resource.ResourceTypeSO.name}' already exists in the manager",
                        nameof(resources));

                _resourcesAmount[resource.ResourceTypeSO] = resource.Amount;
            }
        }


        public int GetAmount(ResourceTypeSO resource)
        {
            if (_resourcesAmount.TryGetValue(resource, out var amount))
                return amount;

            throw new KeyNotFoundException(
                $"Resource '{resource.name}' doesn't exist in the manager");
        }
        
        public ResourceStock[] GetAllResources()
        {
            var resources = new ResourceStock[_resourcesAmount.Count];
            var index = 0;
            foreach (var resource in _resourcesAmount)
            {
                resources[index] = new ResourceStock
                {
                    ResourceTypeSO = resource.Key,
                    Amount = resource.Value
                };
                index++;
            }
            return resources;
        }


        public void AddResources(params ResourceStock[] resources)
        {
            foreach (var resource in resources)
            {
                if (!_resourcesAmount.ContainsKey(resource.ResourceTypeSO))
                    throw new KeyNotFoundException(
                        $"Resource '{resource.ResourceTypeSO.name}' doesn't exist in the manager");

                _resourcesAmount[resource.ResourceTypeSO] += resource.Amount;
                OnResourceAmountChanged?.Invoke(resource.ResourceTypeSO, _resourcesAmount[resource.ResourceTypeSO]);
            }
        }

        public void RemoveResources(params ResourceStock[] resources)
        {
            foreach (var resource in resources)
            {
                if (!_resourcesAmount.ContainsKey(resource.ResourceTypeSO))
                    throw new KeyNotFoundException(
                        $"Resource '{resource.ResourceTypeSO.name}' doesn't exist in the manager");

                if (_resourcesAmount[resource.ResourceTypeSO] < resource.Amount)
                    throw new InvalidOperationException(
                        $"Not enough '{resource.ResourceTypeSO.name}': " +
                        $"has {_resourcesAmount[resource.ResourceTypeSO]}, needs {resource.Amount}");
            }

            foreach (var resource in resources)
            {
                _resourcesAmount[resource.ResourceTypeSO] -= resource.Amount;
                OnResourceAmountChanged?.Invoke(resource.ResourceTypeSO, _resourcesAmount[resource.ResourceTypeSO]);
            }
        }

        public bool CanAfford(params ResourceStock[] cost)
        {
            foreach (var resource in cost)
            {
                if (GetAmount(resource.ResourceTypeSO) < resource.Amount)
                    return false;
            }
            return true;
        }
    }
}
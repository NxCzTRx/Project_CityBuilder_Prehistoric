using _Scripts.BuildSystem;
using _Scripts.Core.DayCycle;
using _Scripts.ResourcesSystem.Resources.ResourceTypes;
using _Scripts.TechTreeSystem.TechEra;
using _Scripts.TechTreeSystem.TechNode;
using UnityEngine;

namespace _Scripts.Events
{
    public interface IEvent
    {
    }

    public struct OnBuildingSelected : IEvent
    {
        public BuildingSO BuildingSo { get; }

        public OnBuildingSelected(BuildingSO buildingSo)
        {
            BuildingSo = buildingSo;
        }
    }

    public struct OnNewDay : IEvent
    {
        public int CurrentDay { get; }

        public OnNewDay(int currentDay)
        {
            CurrentDay = currentDay;
        }
    }

    public struct OnTimeChanged : IEvent
    {
        public GameTime GameTime { get; }

        public OnTimeChanged(int currentHour, int currentMinute)
        {
            GameTime = new GameTime(currentHour, currentMinute);
        }
    }

    public struct OnResourceAmountChanged : IEvent
    {
        public ResourceTypeSO ResourceTypeSO { get; }
        public float Amount { get; }

        public OnResourceAmountChanged(ResourceTypeSO resourceTypeSo, float newAmount)
        {
            ResourceTypeSO = resourceTypeSo;
            Amount = newAmount;
        }
    }

    public struct OnHousingUpdated : IEvent
    {
        public int MaxSpace { get; }
        public int OccupiedSpace { get; }

        public OnHousingUpdated(int maxSpace, int occupiedSpace)
        {
            MaxSpace = maxSpace;
            OccupiedSpace = occupiedSpace;
        }
    }
    
    public struct OnNodeUnlocked : IEvent
    {
        public TechNodeSO Node { get; }
        public OnNodeUnlocked(TechNodeSO node) => Node = node;
    }

    public struct OnEraCompleted : IEvent
    {
        public TechEraSo Era { get; }
        public OnEraCompleted(TechEraSo era) => Era = era;
    }
    
    public struct OnGameNotification : IEvent
    {
        public string Message { get; }
        public float DisplayDuration { get; }

        public OnGameNotification(string message, float displayDuration)
        {
            Message = message;
            DisplayDuration = displayDuration;
        }
    }
}
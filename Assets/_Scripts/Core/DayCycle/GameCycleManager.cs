using System;
using _Scripts.Core.UpdateManagement;
using _Scripts.Events;
using UnityEngine;

namespace _Scripts.Core.DayCycle
{
    public class GameCycleManager : IUpdateObserver
    {
        private const float DayDurationInSeconds = 300;

        private float _currentSeconds = 0;

        private int _currentDay = 0;
        private int _currentHour;
        private int _currentMinute;
        
        private int _lastHour = -1;
        private int _lastMinute = -1;
        
        public event Action<int> OnHourChanged; // PawnScheduler
    
        public void Init()
        {
            UpdateManager.RegisterObserver(this);
        }

        public void ObservedUpdate()
        {
            _currentSeconds += Time.deltaTime;

            if (_currentSeconds >= DayDurationInSeconds)
                _currentSeconds = 0;

            _currentHour = Mathf.FloorToInt(24 * (_currentSeconds / DayDurationInSeconds));
            _currentMinute = Mathf.FloorToInt(
                (24f * (_currentSeconds / DayDurationInSeconds) - _currentHour) * 60);

            if (_currentHour != _lastHour)
            {
                if (_lastHour > _currentHour)
                {
                    _currentDay++;
                    EventBus<OnNewDay>.Publish(new OnNewDay(_currentDay));
                }
                
                _lastHour = _currentHour;
                OnHourChanged?.Invoke(_currentHour);
            }

            if (_currentMinute != _lastMinute)
            {
                _lastMinute = _currentMinute;
                _lastHour = _currentHour;
                EventBus<OnTimeChanged>.Publish(new OnTimeChanged(_currentHour, _currentMinute));
            }
        }

        public GameTime GetTime() => new GameTime(_currentHour, _currentMinute);
    }
}
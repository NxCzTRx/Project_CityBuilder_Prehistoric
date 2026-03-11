using System;
using _Scripts.Core.UpdateManagement;
using UnityEngine;

namespace _Scripts.Core.DayCycle
{
    public class GameCycleManager : IUpdateObserver
    {
        private const float DayDurationInSeconds = 300;

        private float _currentSeconds = 0;
    
        private int _currentHour;
        private int _currentMinute;
        
        private int _lastHour = -1;
        
        public event Action<int> OnHourChanged;
    
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
        
            Debug.Log(_currentHour + ":" + _currentMinute);
            
            if (_currentHour != _lastHour)
            {
                _lastHour = _currentHour;
                OnHourChanged?.Invoke(_currentHour);
            }
        }

        public GameTime GetTime() => new GameTime(_currentHour, _currentMinute);
    }
}

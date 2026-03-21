using System;
using _Scripts.Events;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Gameplay
{
    public class HousingUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text houseAvailabilityTMP;

        private bool _initialized;

        public void Init()
        {
            _initialized = true;
            EventBus<OnHousingUpdated>.Subscribe(UpdateAvailability);
        }
    
        private void OnEnable()
        {
            if (!_initialized) return;
            EventBus<OnHousingUpdated>.Subscribe(UpdateAvailability);
        }

        private void UpdateAvailability(OnHousingUpdated ev)
        {
            houseAvailabilityTMP.text = $"{ev.OccupiedSpace} / {ev.MaxSpace}";
        }
    
        private void OnDisable()
        {
            EventBus<OnHousingUpdated>.Unsubscribe(UpdateAvailability);
        }
    }
}

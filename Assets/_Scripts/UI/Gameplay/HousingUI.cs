using System;
using _Scripts.Events;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Gameplay
{
    public class HousingUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text houseAvailabityTMP;

        private void OnEnable()
        {
            EventBus<OnHousingUpdated>.Subscribe(UpdateAvailabity);
        }

        private void UpdateAvailabity(OnHousingUpdated ev)
        {
            houseAvailabityTMP.text = $"{ev.OccupiedSpace} / {ev.MaxSpace}";
        }
        
        private void OnDisable()
        {
            EventBus<OnHousingUpdated>.Unsubscribe(UpdateAvailabity);
        }
    }
}

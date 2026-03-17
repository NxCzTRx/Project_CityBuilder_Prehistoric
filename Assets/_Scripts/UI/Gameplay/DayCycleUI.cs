using System;
using _Scripts.Events;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Gameplay
{
    public class DayCycleUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text dayTMP;
        [SerializeField] private TMP_Text timeTMP;

        private void OnEnable()
        {
            EventBus<OnNewDay>.Subscribe(UpdateDay);
            EventBus<OnTimeChanged>.Subscribe(UpdateTime);
        }

        private void UpdateDay(OnNewDay eventData)
        {
            dayTMP.text = "Day: " + eventData.CurrentDay.ToString();
        }

        private void UpdateTime(OnTimeChanged eventData)
        {
            timeTMP.text = $"{eventData.GameTime.Hour:D2}:{eventData.GameTime.Minute:D2}";
        }

        private void OnDisable()
        {
            EventBus<OnNewDay>.Unsubscribe(UpdateDay);
            EventBus<OnTimeChanged>.Unsubscribe(UpdateTime);
        }
    }
}

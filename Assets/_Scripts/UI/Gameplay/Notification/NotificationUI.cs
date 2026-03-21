using System.Collections;
using _Scripts.Events;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Gameplay.Notification
{
    public class NotificationUI : MonoBehaviour
    {
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private Transform container;

        public void OnEnable()
        {
            EventBus<OnGameNotification>.Subscribe(OnNotification);
        }

        private void OnNotification(OnGameNotification e)
        {
            var go = Instantiate(notificationPrefab, container);
            go.GetComponentInChildren<TMP_Text>().text = e.Message;
            StartCoroutine(DestroyAfter(go, e.DisplayDuration));
        }

        private IEnumerator DestroyAfter(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(go);
        }

        private void OnDisable()
        {
            EventBus<OnGameNotification>.Unsubscribe(OnNotification);
        }
    }
}

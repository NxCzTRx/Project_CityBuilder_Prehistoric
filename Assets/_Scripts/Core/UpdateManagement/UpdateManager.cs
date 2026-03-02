using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Core.UpdateManagement
{
    public class UpdateManager : MonoBehaviour
    {
        private static List<IUpdateObserver> _updateObservers = new List<IUpdateObserver>();
        private static List<IUpdateObserver> _pendingUpdateObservers = new List<IUpdateObserver>();
        private static int _currentIndex;

        private void Update()
        {
            for (_currentIndex = _updateObservers.Count - 1; _currentIndex >= 0; _currentIndex--)
            {
                _updateObservers[_currentIndex].ObservedUpdate();
            }
            
            _updateObservers.AddRange(_pendingUpdateObservers);
            _pendingUpdateObservers.Clear();
        }

        public static void RegisterObserver(IUpdateObserver updateObserver)
        {
            _pendingUpdateObservers.Add(updateObserver);
        }

        public static void UnregisterObserver(IUpdateObserver updateObserver)
        {
            _updateObservers.Remove(updateObserver);
            _currentIndex--;
        }
    }
}

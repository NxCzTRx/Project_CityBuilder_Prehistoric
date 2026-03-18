using _Scripts.Events;
using _Scripts.TechTreeSystem;
using _Scripts.TechTreeSystem.TechEra;
using UnityEngine;

namespace _Scripts.UI.Gameplay.TechnologyTree
{
    public class TechnologyTreeUI : MonoBehaviour
    {
        private TechEraSo[] _techEras;
        
        [SerializeField] private TechEraSection eraSectionPrefab;
        [SerializeField] private Transform content;

        private TechTreeManager _techTreeManager;

        public void Init(TechTreeManager techTreeManager)
        {
            _techTreeManager = techTreeManager;

            _techEras = _techTreeManager.Eras;

            foreach (var era in _techEras)
            {
                var section = Instantiate(eraSectionPrefab, content);
                section.Init(era, _techTreeManager);
            }

            EventBus<OnEraCompleted>.Subscribe(HandleEraCompleted);
        }

        private void HandleEraCompleted(OnEraCompleted ev)
        {
            foreach (var section in content.GetComponentsInChildren<TechEraSection>())
                section.Refresh();
        }

        public void ToggleTechnologyTree()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        private void OnDestroy() => EventBus<OnEraCompleted>.Unsubscribe(HandleEraCompleted);
    }
}

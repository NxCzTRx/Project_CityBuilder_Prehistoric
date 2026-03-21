using _Scripts.Events;
using _Scripts.TechTreeSystem;
using _Scripts.TechTreeSystem.TechNode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Gameplay.TechnologyTree
{
    public class TechNodeButton : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Button button;
        [SerializeField] private Image unlockedOverlay;

        private TechNodeSO _node;
        private TechTreeManager _techTreeManager;

        public void Init(TechNodeSO node, TechTreeManager techTreeManager)
        {
            _node = node;
            _techTreeManager = techTreeManager;

            icon.sprite = node.Icon;
            nameText.text = node.NodeName;
            costText.text = node.TechCost.ToString();

            button.onClick.AddListener(OnClick);
            EventBus<OnNodeUnlocked>.Subscribe(HandleNodeUnlocked);

            Refresh();
        }

        public void Refresh()
        {
            bool unlocked = _techTreeManager.IsUnlocked(_node);
            bool inCurrentEra = _techTreeManager.IsInCurrentEra(_node);

            button.interactable = inCurrentEra && !unlocked;
            unlockedOverlay.gameObject.SetActive(unlocked);
            costText.gameObject.SetActive(!unlocked);
        }

        private void OnClick()
        {
            _techTreeManager.TryUnlock(_node);
        }
    
        private void HandleNodeUnlocked(OnNodeUnlocked ev)
        {
            if (ev.Node == _node) Refresh();
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClick);
            EventBus<OnNodeUnlocked>.Unsubscribe(HandleNodeUnlocked);
        }
    }
}
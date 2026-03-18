using System.Collections.Generic;
using _Scripts.TechTreeSystem;
using _Scripts.TechTreeSystem.TechEra;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Gameplay.TechnologyTree
{
    public class TechEraSection : MonoBehaviour
    {
        [SerializeField] private TMP_Text eraNameText;
        [SerializeField] private TechNodeButton nodeButtonPrefab;
        [SerializeField] private Transform nodesContainer;

        private readonly List<TechNodeButton> _buttons = new();

        public void Init(TechEraSo era, TechTreeManager techTreeManager)
        {
            eraNameText.text = era.EraName;

            foreach (var node in era.TechNodes)
            {
                var button = Instantiate(nodeButtonPrefab, nodesContainer);
                button.Init(node, techTreeManager);
                _buttons.Add(button);
            }
        }

        public void Refresh()
        {
            foreach (var button in _buttons)
                button.Refresh();
        }
    }
}

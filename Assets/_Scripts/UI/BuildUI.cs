using System;
using _Scripts.BuildSystem;
using _Scripts.Core.GameMode;
using _Scripts.Core.GameMode.Modes;
using _Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class BuildUI : MonoBehaviour
    {
        [SerializeField] private GameObject buildPanel;

        public void ToggleBuildPanel()
        {
            buildPanel.SetActive(!buildPanel.activeSelf);
        }

        public void SelectBuilding(BuildingSO buildingSo)
        {
            EventBus<OnBuildingSelected>.Publish(new OnBuildingSelected(buildingSo));
        }
    }
}

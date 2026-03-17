using _Scripts.BuildSystem;
using _Scripts.Events;
using UnityEngine;

namespace _Scripts.UI.Gameplay
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

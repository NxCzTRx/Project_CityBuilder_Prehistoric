using _Scripts.BuildSystem;
using _Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Gameplay.Build
{
    public class BuildUI : MonoBehaviour
    {
        [SerializeField] private GameObject buildPanel;
        [SerializeField] private BuildingButton buttonPrefab;

        public void ToggleBuildPanel()
        {
            buildPanel.SetActive(!buildPanel.activeSelf);
        }

        public void SelectBuilding(BuildingSO buildingSo)
        {
            EventBus<OnBuildingSelected>.Publish(new OnBuildingSelected(buildingSo));
        }

        public void AddNewBuilding(BuildingSO buildingSo)
        {
            var button = Instantiate(buttonPrefab, buildPanel.transform);
    
            if (button.TryGetComponent<Button>(out var btn))
                btn.onClick.AddListener(() => SelectBuilding(buildingSo));
            else
                Debug.LogWarning($"Button prefab missing Button component");
            
            button.Init(buildingSo);
        }
    }
}

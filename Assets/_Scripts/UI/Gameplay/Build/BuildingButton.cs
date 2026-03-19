using System.Text;
using _Scripts.BuildSystem;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Gameplay.Build
{
    public class BuildingButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text buildingName;
        [SerializeField] private TMP_Text buildingCost;
    
        public void Init(BuildingSO buildingSO)
        {
            buildingName.text = buildingSO.BuildingName;

            var costText = new StringBuilder();
            
            foreach (var item in buildingSO.BuildingCost)
                costText.Append($"{item.ResourceTypeSO.ResourceName}: {item.Amount} ");

            buildingCost.text = costText.ToString();
        }  
    }
}

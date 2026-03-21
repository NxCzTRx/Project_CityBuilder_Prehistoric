using _Scripts.ResourcesSystem.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Gameplay
{
    public class ResourceDataUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text resourceType;
        [SerializeField] private TMP_Text resourceAmount;
        public void SetUp(ResourceStock stock)
        {
            resourceType.text = stock.ResourceTypeSO.ResourceName;
            resourceAmount.text = stock.Amount.ToString();
        }
    
        public void UpdateAmount(float amount)
        {
            resourceAmount.text = Mathf.FloorToInt(amount).ToString();
        }
    }
}

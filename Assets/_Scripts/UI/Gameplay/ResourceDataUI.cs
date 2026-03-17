using _Scripts.ResourcesSystem.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDataUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text resourceAmount;
    public void SetUp(ResourceStock stock)
    {
        icon.sprite = stock.ResourceTypeSO.ResourceIcon;
        resourceAmount.text = stock.Amount.ToString();
    }
    
    public void UpdateAmount(float amount)
    {
        resourceAmount.text = Mathf.FloorToInt(amount).ToString();
    }
}

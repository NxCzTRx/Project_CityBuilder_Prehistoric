using _Scripts.ResourcesSystem.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceRowUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text resourceName;
    [SerializeField] private TMP_Text resourceAmount;
    public void SetUp(ResourceStock stock)
    {
        icon.sprite = stock.ResourceTypeSO.ResourceIcon;
        resourceName.text = stock.ResourceTypeSO.ResourceName;
        resourceAmount.text = stock.Amount.ToString();
    }
    
    public void UpdateAmount(int amount)
    {
        resourceAmount.text = amount.ToString();
    }
}

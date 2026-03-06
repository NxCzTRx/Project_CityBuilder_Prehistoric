using System;
using UnityEngine;

namespace _Scripts.BuildSystem.Building
{
    public class BuildingView : MonoBehaviour, ISelectable
    {
        [SerializeField] BuildingHUD buildingHUD;
        
        public void Init(BuildingController controller, PawnRegistry pawnRegistry)
        {
            buildingHUD.Init(controller, pawnRegistry);
        }

        public void Select()
        {
            buildingHUD.gameObject.SetActive(true);
        }

        public void Unselect()
        {
            buildingHUD.gameObject.SetActive(false);
        }
    }
}

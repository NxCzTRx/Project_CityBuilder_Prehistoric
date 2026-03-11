using _Scripts.Core;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.BuildSystem.Building.Housing
{
    [RequireComponent(typeof(HouseView))]
    public class HouseEntity : MonoBehaviour
    {
        HouseController _controller;
    
        public void Init(HouseSO houseSO, Cell entranceCell, ObjectResolver objectResolver)
        {
            var model = new HouseModel(houseSO, entranceCell);
            var view = GetComponent<HouseView>();

            _controller = new HouseController(model, view);
            objectResolver.Resolve<HousingRegistry>().RegisterHouse(_controller);
        }
    }
}

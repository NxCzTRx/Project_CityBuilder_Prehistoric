using System;
using UnityEngine;

namespace _Scripts.BuildSystem.InitialBuildings
{
    [Serializable]
    public struct InitialBuilding 
    {
        public BuildingSO BuildingSo;
        public Vector2Int GridOrigin;
    }
}

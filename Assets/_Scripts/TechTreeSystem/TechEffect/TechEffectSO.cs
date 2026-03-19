using _Scripts.Core;
using UnityEngine;

namespace _Scripts.TechTreeSystem.TechEffect
{
    public abstract class TechEffectSO : ScriptableObject 
    {
        public abstract void Apply(ObjectResolver resolver);
    }
}

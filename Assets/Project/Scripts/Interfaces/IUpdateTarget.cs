using UnityEngine;

namespace TZ.Enemy_FSM.Interfaces
{
    public interface IUpdateTarget
    {
        public void UpdateTarget(Transform newTarget);
    }
}
using TZ.Enemy_FSM.Interfaces;
using UnityEngine;

namespace TZ.Enemy
{
    public class EnemyBaseController : MonoBehaviour, ITakeRources
    {
        [SerializeField] private int _currentResourcesCount;
        
        public void TakeResource(int resource)
        {
            _currentResourcesCount += resource;
        }
    }
}
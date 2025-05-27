using TZ.Enemy_FSM.Interfaces;
using UnityEngine;

namespace TZ.Enemy
{
    public class EnemyBaseController : MonoBehaviour, ITakeRources
    {
        [SerializeField] private EFractions _fractionBase;
        [SerializeField] private int _currentResourcesCount;

        public EFractions FractionBase => _fractionBase;
        
        public void TakeResource(int resource)
        {
            _currentResourcesCount += resource;
        }
    }
}
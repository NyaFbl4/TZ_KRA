using TZ.Enemy;
using TZ.Enemy_FSM.Interfaces;
using UnityEngine;

namespace TZ.Enemy_FSM
{
    public class EnemyStatePutFSM : EnemyStateFSM, IPutResources , ITakeRources ,IUpdateTarget
    {
        private Transform _target;
        private int _resourcesCount;

        public EnemyStatePutFSM(EnemyFSM enemyFsm, int resourcesCount) : base(enemyFsm)
        {
            _resourcesCount = resourcesCount;
        }

        public override void EnterState()
        {
            Debug.Log("Enter EnemyStatePutFSM");
            if (_target != null)
            {
                var targetPoint = _target.GetComponent<ITakeRources>();

                targetPoint.TakeResource(PutResources());
            }
        }

        public override void ExitState()
        {
            Debug.Log("Exit EnemyStatePutFSM");
        }

        public override void UpdateState()
        {
            
        }
        
        public int PutResources()
        {
            Debug.Log( "EnemyStatePutFSM put " + _resourcesCount);
            
            return _resourcesCount;
        }

        public void UpdateTarget(Transform newTarget)
        {
            _target = newTarget;
        }

        public void TakeResource(int resource)
        {
            _resourcesCount = resource;
        }
    }
}
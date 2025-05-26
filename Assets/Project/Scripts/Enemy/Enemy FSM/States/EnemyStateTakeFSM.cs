using System;
using TZ.Enemy_FSM.Interfaces;
using TZ.Resource;
using UnityEngine;

namespace TZ.Enemy_FSM
{
    public class EnemyStateTakeFSM : EnemyStateFSM, ITakeRources, IUpdateTarget
    {
        private Transform _transform;
       // private Transform _resourcePoint;
        private Transform _targetResource;
        private int _resourceCount;
        private ITakeRources _takeRourcesImplementation;

        public event Action<int> OnResurceTake;
        
        public EnemyStateTakeFSM(EnemyFSM enemyFsm, Transform transform) : base(enemyFsm)
        {
            _transform = transform;
        }

        public override void EnterState()
        {
            Debug.Log("ENTER EnemyStateTakeFSM");
        }

        public override void ExitState()
        {
            Debug.Log("EXIT EnemyStateTakeFSM");
        }

        public override void UpdateState()
        {
            TakeResource(_targetResource.gameObject);
        }

        public void UpdateTarget(Transform newTarget)
        {
            _targetResource = newTarget;
        }
        
        public void TakeResource(GameObject resource)
        {
            if (resource != null)
            {
                var resourcePoint = resource.GetComponent<ResourcePoint>();

                if (resourcePoint != null)
                {
                    _resourceCount = resourcePoint.PutResources();
                    OnResurceTake?.Invoke(_resourceCount);
                }
            }
            else
            {
                Debug.Log("net tergeta");
            }
        }
    }
}
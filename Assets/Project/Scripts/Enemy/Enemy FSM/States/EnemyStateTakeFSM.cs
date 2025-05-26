using System;
using TZ.Enemy_FSM.Interfaces;
using TZ.Resource;
using UnityEngine;

namespace TZ.Enemy_FSM
{
    public class EnemyStateTakeFSM : EnemyStateFSM, ITakeRources, IUpdateTarget
    {
        private GameObject _resurceObj;
       // private Transform _resourcePoint;
        private Transform _targetResource;
        private int _resourceCount;
        private ITakeRources _takeRourcesImplementation;

        public event Action<int> OnResurceTake;
        
        public EnemyStateTakeFSM(EnemyFSM enemyFsm) : base(enemyFsm)
        {
        }

        public override void EnterState()
        {
            Debug.Log("ENTER EnemyStateTakeFSM");
            
            if (_targetResource != null)
            {
                var resourcePoint = _targetResource.GetComponent<ResourcePoint>();

                TakeResource(resourcePoint.PutResources());
            }
        }

        public override void ExitState()
        {
            Debug.Log("EXIT EnemyStateTakeFSM");
        }

        public override void UpdateState()
        {
            
        }

        public void UpdateTarget(Transform newTarget)
        {
            _targetResource = newTarget;
        }

        public void TakeResource(int resource)
        {
            _resourceCount = resource;
            OnResurceTake?.Invoke(_resourceCount);
        }
    }
}
using System;
using TZ.Enemy_FSM;
using TZ.Enemy_FSM.Interfaces;
using UnityEngine;

namespace TZ.Enemy
{
    public class EnemyController : MonoBehaviour, IUpdateTarget
    {
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _stoppingDistance;
        [SerializeField] private Transform _targetMove;
        [SerializeField] private GameObject _targetTake;
        [SerializeField] private GameObject _baseFraction;
        //[SerializeField] private Transform _resourcesPoint;
        [SerializeField] private int _resourcesCount;
        

        private EnemyFSM _fsm;
        private EnemyStateIdleFSM _stateIdle;
        private EnemyStateWalkFSM _stateWalk;
        private EnemyStateTakeFSM _stateTake;
        private EnemyStatePutFSM  _statePut;
        
        public event Action<int> OnPutResoutces;

        private void Start()
        {
            _fsm = new EnemyFSM();

            _stateIdle = new EnemyStateIdleFSM(_fsm);
            _fsm.AddState(_stateIdle);
            
            _stateWalk = new EnemyStateWalkFSM(_fsm, gameObject.transform, _targetMove.transform,
                _walkSpeed, _stoppingDistance, true);
            _fsm.AddState(_stateWalk);
            
            _stateTake = new EnemyStateTakeFSM(_fsm, gameObject.transform);
            _fsm.AddState(_stateTake);
            
            _statePut = new EnemyStatePutFSM(_fsm, _resourcesCount);
            _fsm.AddState(_statePut);
            
            _stateTake.OnResurceTake += HandleResourceTake;
            _stateWalk.OnReachedTarget += HandleReachedTarget;
            
            _fsm.SetState<EnemyStateWalkFSM>();
        }

        private void OnDisable()
        {
            _stateTake.OnResurceTake -= HandleResourceTake;
            _stateWalk.OnReachedTarget -= HandleReachedTarget;
        }

        private void Update()
        {
            _fsm.Update();
        }
        
        private void HandleReachedTarget(Transform transform)
        {
            Debug.Log("HandleReachedTarget");
            
            if (transform == null)
            {
                return;
            }

            GameObject gameObject = transform.gameObject;

            if (gameObject.tag == "Resource")
            {
                Debug.Log(gameObject.tag);
                _stateTake.UpdateTarget(_targetMove);
                _fsm.SetState<EnemyStateTakeFSM>();
            }
            else if (gameObject.tag == "EnemyBase")
            {
                _fsm.SetState<EnemyStatePutFSM>();

                OnPutResoutces?.Invoke(_resourcesCount);
            }
        }
        
        private void HandleResourceTake(int newResources)
        {
            _stateWalk.UpdateTarget(_baseFraction.transform);
            _statePut.UpdateResources(newResources);
            UpdateTarget(_baseFraction.transform);
        }

        public void UpdateTarget(Transform newTarget)
        {
            if (newTarget != null)
            {
                _targetMove = newTarget;

                if (newTarget != _baseFraction)
                {
                    //_targetTake = newTarget;
                }

                if (_stateWalk != null)
                {
                    _stateWalk.UpdateTarget(_targetMove);
                    _fsm.SetState<EnemyStateWalkFSM>();
                }
            }
        }
    }
}
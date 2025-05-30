﻿using System;
using TZ.Enemy_FSM;
using TZ.Enemy_FSM.Interfaces;
using UnityEngine;

namespace TZ.Enemy
{
    public class EnemyController : MonoBehaviour, IUpdateTarget
    {
        [SerializeField] private EFractions _fractionEnemy;
        [SerializeField] private NearestResourceFinder _findNearest;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _stoppingDistance;
        [SerializeField] private Transform _targetMove;
        [SerializeField] private GameObject _baseFraction;
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
            
            _stateTake = new EnemyStateTakeFSM(_fsm);
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
                Debug.Log(gameObject.tag);
                _fsm.SetState<EnemyStatePutFSM>();
                OnPutResoutces?.Invoke(_resourcesCount);
                _resourcesCount = 0;
                UpdateTarget(_findNearest.FindNearest().transform);
            }
        }
        
        private void HandleResourceTake(int newResources)
        {
            _resourcesCount += newResources;
            
            _stateWalk.UpdateTarget(_baseFraction.transform);
            _statePut.UpdateTarget(_baseFraction.transform);
            _statePut.TakeResource(newResources);
            UpdateTarget(_baseFraction.transform);
        }

        public void UpdateTarget(Transform newTarget)
        {
            if (newTarget != null)
            {
                _targetMove = newTarget;
            }
            else
            {
                _targetMove = _findNearest.FindNearest().transform;
            }
            
            if (_stateWalk != null)
            {
                _stateWalk.UpdateTarget(_targetMove);
                _fsm.SetState<EnemyStateWalkFSM>();
            }
        }
    }
}
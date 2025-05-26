using System;
using TZ.Enemy_FSM.Interfaces;
using UnityEngine;

namespace TZ.Enemy_FSM
{
    public class EnemyStateWalkFSM : EnemyStateFSM, IUpdateTarget
    {
        private Transform _transform;
        private Transform _targetTransform;
        private Transform _baseTransform;

        private float _speed;
        private float _stoppingDistance;

        public event Action<Transform> OnReachedTarget; 
        
        public EnemyStateWalkFSM(EnemyFSM enemyFsm, Transform transform, 
            Transform targetTransform, float speed, float stoppingDistance, bool isMoving) : base(enemyFsm)
        {
            _transform = transform;
            _targetTransform = targetTransform;
            _speed = speed;
            _stoppingDistance = stoppingDistance;
            base.isMoving = isMoving;
        }
        
        public override void EnterState()
        {
            Debug.Log("Enter EnemyStateWalkFSM");
        }
        
        public override void ExitState()
        {
            Debug.Log("Exit EnemyStateWalkFSM");
        }
        
        public override void UpdateState()
        {
            Debug.Log("Update EnemyStateWalkFSM");

            if (!isMoving)
            {
                EnemyFsm.SetState<EnemyStateIdleFSM>();
            }
            
            MoveTowardsTarget();
        }

        public void UpdateTarget(Transform newTarget)
        {
            if (newTarget == null)
            {
                SwitchIsMoving(false);
                
                return;
            }

            _targetTransform = newTarget;
            SwitchIsMoving(true);
        }

        private void MoveTowardsTarget()
        {
            Vector2 currentPos = _transform.position;
            Vector2 targetPos = _targetTransform.position;

            Vector2 direction = (targetPos - currentPos).normalized;
            
            // Если дистанция больше stoppingDistance — двигаемся
            if (Vector2.Distance(currentPos, targetPos) > _stoppingDistance)
            {
                // Двигаемся по направлению с учетом скорости
                Vector2 newPosition = currentPos + direction * _speed * Time.deltaTime;
                _transform.position = newPosition;
            }
            else
            {
                Debug.Log("уже достаточно близко");
                OnReachedTarget?.Invoke(_targetTransform);
            }
            
            /*
            Vector3 newPosition = Vector3.MoveTowards(
                currentPos,
                targetPos,  // Без фиксации Y, двигаемся к полной позиции цели
                _speed * Time.deltaTime
            );

            _transform.position = newPosition;

            float distance = Vector2.Distance(
                new Vector2(_transform.position.x, _transform.position.y),
                new Vector2(targetPos.x, targetPos.y));

            if (distance < 0.1f)
            {
                OnReachedTarget?.Invoke(_targetTransform);
            }
            */
        }
    }
}
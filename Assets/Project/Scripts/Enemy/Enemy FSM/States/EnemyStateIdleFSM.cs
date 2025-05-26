using UnityEngine;

namespace TZ.Enemy_FSM
{
    public class EnemyStateIdleFSM : EnemyStateFSM
    {
        private GameObject _target;
        
        public EnemyStateIdleFSM(EnemyFSM enemyFsm) : base(enemyFsm) { }
        
        public override void EnterState()
        {
            Debug.Log("Enter EnemyStateIdleFSM");
        }
        
        public override void ExitState()
        {
            Debug.Log("Exit EnemyStateIdleFSM");
        }
        
        public override void UpdateState()
        {
            if (isMoving)
            {
                EnemyFsm.SetState<EnemyStateWalkFSM>();
            }
            
            Debug.Log("Update EnemyStateIdleFSM");
        }
    }
}
namespace TZ.Enemy_FSM
{
    public abstract class EnemyStateFSM
    {
        protected readonly EnemyFSM EnemyFsm;
        protected bool isMoving = false;

        public EnemyStateFSM(EnemyFSM enemyFsm)
        {
            EnemyFsm = enemyFsm;
        }

        public void SwitchIsMoving(bool meaning)
        {
            isMoving = meaning;
        }
        
        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void UpdateState() { }
    }
}
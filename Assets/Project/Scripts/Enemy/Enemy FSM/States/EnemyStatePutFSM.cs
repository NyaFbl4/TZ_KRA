using TZ.Enemy_FSM.Interfaces;
using UnityEngine;

namespace TZ.Enemy_FSM
{
    public class EnemyStatePutFSM : EnemyStateFSM, IPutRecources, IUpdateResources
    {
        private int _resourcesCount;

        public EnemyStatePutFSM(EnemyFSM enemyFsm, int resourcesCount) : base(enemyFsm)
        {
            _resourcesCount = resourcesCount;
        }

        public override void EnterState()
        {
            Debug.Log("Enter EnemyStatePutFSM");
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

        public void UpdateResources(int newResources)
        {
            _resourcesCount = newResources;
        }
    }
}
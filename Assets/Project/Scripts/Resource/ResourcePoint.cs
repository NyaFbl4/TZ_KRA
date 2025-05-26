using TZ.Enemy_FSM.Interfaces;
using UnityEngine;

namespace TZ.Resource
{
    public class ResourcePoint : MonoBehaviour, IPutRecources
    {
        [SerializeField] private int _resourcesCount; 
        
        public int PutResources()
        {
            Debug.Log("ResourcePoint put " + _resourcesCount);
            
            return _resourcesCount;
        }
    }
}
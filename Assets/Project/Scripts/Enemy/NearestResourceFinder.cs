using System.Collections.Generic;
using System.Linq;
using TZ.Enemy_FSM.Interfaces;
using TZ.Resource;
using UnityEngine;

namespace TZ.Enemy
{
    public class NearestResourceFinder : MonoBehaviour, IFindNearest
    {
        [SerializeField] private float _searchingRadius;
        
        public GameObject FindNearest()
        {
            var resource = FindObjectsOfType<ResourcePoint>()
                .Where(resourcePoint => Vector3.Distance(transform.position, 
                    resourcePoint.transform.position) <= _searchingRadius)
                .ToArray();
            

            if (resource.Length == 0)
            {
                Debug.Log("No ResourcePoint found in radius");
                return null;
            }
            
            return resource
                .OrderBy(resourcePoint => Vector3.Distance(transform.position, 
                    resourcePoint.transform.position))
                .First()
                .gameObject;
        }
    }
}
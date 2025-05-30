﻿using TZ.Enemy_FSM.Interfaces;
using UnityEngine;

namespace TZ.Resource
{
    public class ResourcePoint : MonoBehaviour, IPutResources 
    {
        [SerializeField] private int _resourcesCount; 
        
        public int PutResources()
        {
            Destroy(gameObject);
            return _resourcesCount;
        }
    }
}
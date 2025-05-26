using TZ.Enemy;
using TZ.Enemy_FSM;
using UnityEngine;

namespace TZ
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private EnemyController _controller;
        [SerializeField] private GameObject _resource;

        private void Start()
        {
            _controller.UpdateTarget(_resource.transform);
        }
    }
}
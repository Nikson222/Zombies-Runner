using System;
using CarStates;
using UnityEngine;
using StateMachines;

namespace CarControl
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarMovement : MonoBehaviour
    {
        private const int MAX_SPEED = 50;
        
        private Rigidbody _rigidbody;
        private float _currentSpeed;
        
        [Header("Speed Settings")]
        [SerializeField] private float _idleSpeed;
        [SerializeField] private float _launchingSpeed;
        [SerializeField] private float _launchedSpeed;
        
        public float CurrentSpeed
        {
            get => _currentSpeed;
            set
            {
                if(value > 0 && value < MAX_SPEED)
                    _currentSpeed = value;
            }
        }

        public float IdleSpeed => _idleSpeed;
        public float LaunchingSpeed => _launchingSpeed;
        public float LaunchedSpeed => _launchedSpeed;
        

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        

        private void FixedUpdate()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            _rigidbody.MovePosition(_rigidbody.position + transform.forward * CurrentSpeed * Time.fixedDeltaTime);
        }
    }
}

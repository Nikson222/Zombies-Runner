using System;
using CarStates;
using StateMachines;
using Datas;
using TurretControl;
using UnityEngine;

namespace CarControl
{
    public class Car : MonoBehaviour, IDamageable
    {
        private const string LAUNCH_TRIGGER_TAG = "LaunchTrigger";
        
        private StateMachine _stateMachine;
        
        private IdleCarState _idleState;
        private LaunchingCarState _launchingState;
        private LaunchedCarState _launchedState;

        [SerializeField] private float _health;
            
        [SerializeField] private CarMovement _carMovement;
        [SerializeField] private Turret _turret;
        
        public event Action OnLaunch;
        public LaunchingCarState LaunchingState => _launchingState;
        public LaunchedCarState LaunchedState => _launchedState;
        
        public float Health => _health;
        
        public CarMovement CarMovement => _carMovement;
        private void Awake()
        {
            InitStateMachine();
        }
        
        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }
        
        public void GetDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                print("Car Damaged");
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(LAUNCH_TRIGGER_TAG))
            {
                OnLaunch?.Invoke();
                _turret.Launch();
            }
        }
        
        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();
            
            _idleState = new IdleCarState(_stateMachine, this);
            _launchingState = new LaunchingCarState(_stateMachine, this);
            _launchedState = new LaunchedCarState(_stateMachine, this);
            
            _stateMachine.Initialize(_idleState);
        }
    }
}
using CarControl;
using StateMachines;
using UnityEngine;

namespace CarStates
{
    public class LaunchingCarState : IState
    {
        Car _car;
        StateMachine _stateMachine;
        
        public LaunchingCarState(StateMachine stateMachine, Car car)
        {
            _car = car;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _car.CarMovement.CurrentSpeed = _car.CarMovement.LaunchingSpeed;
            _car.OnLaunch += () => { _stateMachine.ChangeState(_car.LaunchedState); };
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            _car.OnLaunch -= () => { _stateMachine.ChangeState(_car.LaunchedState); };
        }
    }
}
using CarControl;
using StateMachines;
using UnityEngine;

namespace CarStates
{
    public class LaunchedCarState : IState
    {
        Car _car;
        StateMachine _stateMachine;
        
        public LaunchedCarState(StateMachine stateMachine, Car car)
        {
            _car = car;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _car.CarMovement.CurrentSpeed = _car.CarMovement.LaunchedSpeed;
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}
using CarControl;
using StateMachines;
using UnityEngine;

namespace CarStates
{
    public class IdleCarState : IState
    {
        Car _car;
        StateMachine _stateMachine;
        
        public IdleCarState(StateMachine stateMachine, Car car)
        {
            _car = car;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _car.CarMovement.CurrentSpeed = _car.CarMovement.IdleSpeed;
        }

        public void Update()
        {
            if(Input.touchCount > 0)
               _stateMachine.ChangeState(_car.LaunchingState);
        }

        public void Exit()
        {
            
        }
    }
}
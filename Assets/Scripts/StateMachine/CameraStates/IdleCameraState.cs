using CameraControl;
using Cinemachine;
using StateMachines;
using UnityEngine;

namespace CameraStates
{
    public class IdleCameraState : IState
    {
        private StateMachine _stateMachine;
        private FollowCamera _followCamera;
        
        public IdleCameraState(StateMachine stateMachine, FollowCamera followCamera)
        {
            _stateMachine = stateMachine;
            _followCamera = followCamera;
        }
        
        public void Enter()
        {
            _followCamera.ClearAnimation();
        }

        public void Update()
        {
            if(Input.touchCount > 0)
                _stateMachine.ChangeState(_followCamera.LaunchingState);
        }

        public void Exit()
        {
            
        }
    }
}
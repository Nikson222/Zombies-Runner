using CameraControl;
using StateMachines;

namespace CameraStates
{
    public class LaunchingCameraState: IState
    {
        private StateMachine _stateMachine;
        private FollowCamera _followCamera;
        
        public LaunchingCameraState(StateMachine stateMachine, FollowCamera followCamera)
        {
            _stateMachine = stateMachine;
            _followCamera = followCamera;
        }
        
        public void Enter()
        {
            _followCamera.EnableLaunchingCameraAnimation();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}
using CameraStates;
using Cinemachine;
using StateMachines;
using UnityEngine;

namespace CameraControl
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FollowCamera : MonoBehaviour
    {
        private const string LAUNCHING_ANIM_PARAMETR = "IsLaunching";
    
        private Transform _target;
        private StateMachine _stateMachine;

        private IdleCameraState _idleState;
        private LaunchingCameraState _launchingState;

        [SerializeField] private Animator _animator;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    
        public LaunchingCameraState LaunchingState => _launchingState;
    
    
        public void Init(Transform target)
        {
            InitStateMachine();

            SetTarget(target);
        }
    
        public void ClearAnimation()
        {
            _animator.SetBool(LAUNCHING_ANIM_PARAMETR, false);
        }
    
        public void EnableLaunchingCameraAnimation() => _animator.SetBool(LAUNCHING_ANIM_PARAMETR, true);

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();
        
            _idleState = new IdleCameraState(_stateMachine, this);
            _launchingState = new LaunchingCameraState(_stateMachine, this);
        
            _stateMachine.Initialize(_idleState);
        }

        private void SetTarget(Transform target)
        {
            _target = target;
        
            _virtualCamera.Follow = _target;
            _virtualCamera.LookAt = _target;
        }
    }
}

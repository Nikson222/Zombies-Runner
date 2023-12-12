using Datas;
using StateMachines;
using TurretStates;
using UnityEngine;
using Zenject;

namespace TurretControl
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private LineRenderer _aimingLine;
        
        [SerializeField] private TurretRotate _turretRotate;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private TurretShoot _turretShoot;
        
        private StateMachine _stateMachine;
        
        private IdleTurretState _idleState;
        private LaunchingTurretState _launchingState;
        private LaunchedTurretState _launchedState;
        
        private bool _isLaunched;
        
        public TurretRotate TurretRotate => _turretRotate;
        public TurretShoot TurretShoot => _turretShoot;
        public LineRenderer AimingLine => _aimingLine;
        public LaunchingTurretState LaunchingState => _launchingState;
        public LaunchedTurretState LaunchedState => _launchedState;
        public bool IsLaunched => _isLaunched;

        
        private void Awake()
        {
            InitStateMachine();
            
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
            
            _turretShoot.Shoot(_bulletSpawnPoint);
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();

            _idleState = new IdleTurretState(_stateMachine, this);
            _launchingState = new LaunchingTurretState(_stateMachine, this);
            _launchedState = new LaunchedTurretState(_stateMachine, this);

            _stateMachine.Initialize(_idleState);
        }
        
        public void Launch()
        {
            _isLaunched = true;
        }
    }
}
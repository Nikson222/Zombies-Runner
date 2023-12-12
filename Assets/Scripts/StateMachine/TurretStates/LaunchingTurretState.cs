using StateMachines;
using Datas;
using TurretControl;
using UnityEngine;

namespace TurretStates
{
    public class LaunchingTurretState : IState
    {
        private StateMachine _stateMachine;
        private Turret _turret;
        
        public LaunchingTurretState(StateMachine stateMachine, Turret turret)
        {
            _stateMachine = stateMachine;
            _turret = turret;
        }
        
        public void Enter()
        {
            _turret.TurretRotate.UnblockRotation();
        }

        public void Update()
        {
            if(_turret.IsLaunched)
                _stateMachine.ChangeState(_turret.LaunchedState);
        }

        public void Exit()
        {
            
        }
    }
}
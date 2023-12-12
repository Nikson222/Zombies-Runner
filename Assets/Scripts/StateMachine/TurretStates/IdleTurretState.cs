using StateMachines;
using Datas;
using TurretControl;
using UnityEngine;

namespace TurretStates
{
    public class IdleTurretState : IState
    {
        private StateMachine _stateMachine;
        private Turret _turret;
        
        public IdleTurretState(StateMachine stateMachine, Turret turret)
        {
            _stateMachine = stateMachine;
            _turret = turret;
        }
        
        public void Enter()
        {
            _turret.TurretRotate.BlockRotation();
            _turret.TurretShoot.BlockShoot();
            _turret.AimingLine.enabled = false;
        }

        public void Update()
        {
            if(Input.touchCount > 0)
                _stateMachine.ChangeState(_turret.LaunchingState);
        }

        public void Exit()
        {
            
        }
    }
}
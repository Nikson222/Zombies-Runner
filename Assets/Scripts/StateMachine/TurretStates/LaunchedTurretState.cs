using StateMachines;
using Datas;
using TurretControl;
using UnityEngine;

namespace TurretStates
{
    public class LaunchedTurretState: IState
    {
        private StateMachine _stateMachine;
        private Turret _turret;
        
        public LaunchedTurretState(StateMachine stateMachine, Turret turret)
        {
            _stateMachine = stateMachine;
            _turret = turret;
        }
        
        public void Enter()
        {
            _turret.TurretShoot.UnblockShoot();
            _turret.AimingLine.enabled = true;
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}
using CarControl;
using Enemies;
using UnityEngine;

namespace StateMachines.EnemyStates
{
    public class IdleEnemyState : IState
    {
        private const string CAR_LAYER_NAME = "Car";
        private readonly StateMachine _stateMachine;
        private readonly Enemy _enemy;

        public IdleEnemyState(StateMachine stateMachine, Enemy enemy)
        {
            _stateMachine = stateMachine;
            _enemy = enemy;
        }

        public void Enter()
        {
            
        }

        public void Update()
        {
            FindPlayerCar();
        }

        public void Exit()
        {
        }

        private void FindPlayerCar()
        {
            if(_enemy.IsHitProcess || _enemy.IsExploiseProcess)
                return;
            
            Collider[] car = Physics.OverlapSphere(_enemy.transform.position, _enemy.CarDetectionRadius,
                LayerMask.GetMask(CAR_LAYER_NAME));

            if (car.Length > 0)
            {
                Car targetCar = car[0].GetComponent<Car>();
                IDamageable targetDamageableComponent = targetCar.GetComponent<IDamageable>();

                if (targetCar != null && targetDamageableComponent != null)
                {
                    _enemy.SetTarget(targetCar, targetDamageableComponent);
                    _stateMachine.ChangeState(_enemy.AggressiveState);
                }
            }
        }
    }
}
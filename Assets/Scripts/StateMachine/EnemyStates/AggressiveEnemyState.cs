using Enemies;
using UnityEngine;

namespace StateMachines.EnemyStates
{
    public class AggressiveEnemyState : IState
    {
        private const string RUN_PARAMETER_NAME = "IsRunning";
        private const string ATTACK_PARAMETER_NAME = "IsAttack";

        private readonly StateMachine _stateMachine;
        private readonly Enemy _enemy;

        private float delayAttackTimer;
        private bool _isAlreadyAttacked;

        public AggressiveEnemyState(StateMachine stateMachine, Enemy enemy)
        {
            _stateMachine = stateMachine;
            _enemy = enemy;
        }

        public void Enter()
        {
            Debug.Log("Aggressive");

            _enemy.IsStateHaveFixedUpdateMethods = true;
        }

        public void Update()
        {
            if (_isAlreadyAttacked && delayAttackTimer <= 0)
            {
                _stateMachine.ChangeState(_enemy.IdleState);
                _enemy.Exploise();
            }
            
            if (delayAttackTimer <= 0)
            {
                _enemy.Animator.SetBool(ATTACK_PARAMETER_NAME, false);
                
                if (!_enemy.IsEnemyCollisionCar)
                    MoveToTarget();

                if (_enemy.IsEnemyCollisionCar)
                    AttackCar();
            }
            else
                delayAttackTimer -= Time.deltaTime;
        }

        public void Exit()
        {
            _enemy.IsStateHaveFixedUpdateMethods = false;
            _enemy.Animator.SetBool(RUN_PARAMETER_NAME, false);
            _enemy.Animator.SetBool(ATTACK_PARAMETER_NAME, false);
        }

        private void MoveToTarget()
        {
            if (_enemy.TargetCar != null)
            {
                Vector3 direction = (_enemy.TargetCar.transform.position - _enemy.transform.position).normalized;

                _enemy.Rigidbody.MovePosition(
                    _enemy.Rigidbody.position + direction * _enemy.Speed * Time.fixedDeltaTime);
                _enemy.transform.LookAt(_enemy.TargetCar.transform);
                
                _enemy.Animator.SetBool(RUN_PARAMETER_NAME, true);
            }
            else
                _stateMachine.ChangeState(_enemy.IdleState);
        }

        private void AttackCar()
        {
            _enemy.Animator.SetBool(ATTACK_PARAMETER_NAME, true);
            _enemy.TargetDamageableComponent.GetDamage(_enemy.Damage);
            delayAttackTimer = (_enemy.Animator.GetCurrentAnimatorStateInfo(0).length 
                               / _enemy.Animator.GetCurrentAnimatorStateInfo(0).speed) / 3f;
            _isAlreadyAttacked = true;
        }
    }
}
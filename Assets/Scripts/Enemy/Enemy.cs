using System;
using System.Collections;
using CarControl;
using StateMachines;
using StateMachines.EnemyStates;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        private const string HIT_PARAMETER_NAME = "IsHit";

        private StateMachine _stateMachine;
        
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _carDetectionRadius;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _stickmanGameObject;
        [SerializeField] private ParticleSystem _exploiseParticle;

        [SerializeField] private Rigidbody _rigidbody;
        
        private IdleEnemyState _idleState;
        private AggressiveEnemyState _aggressiveState;
        
        private Car _targetCar;
        private IDamageable _targedDamageableComponent;

        private bool _isHitProcess = false;
        private bool _isExploiseProcess = false;
        
        public float Health => _health;
        public float Speed => _speed;
        public float Damage => _damage;
        public float CarDetectionRadius => _carDetectionRadius;
        public Animator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;
        public Car TargetCar => _targetCar;
        public IDamageable TargetDamageableComponent => _targedDamageableComponent;
        public IdleEnemyState IdleState => _idleState;
        public AggressiveEnemyState AggressiveState => _aggressiveState;

        public bool IsStateHaveFixedUpdateMethods;
        public bool IsEnemyCollisionCar;
        public bool IsHitProcess => _isHitProcess;
        public bool IsExploiseProcess => _isExploiseProcess;

        private void Awake()
        {
            InitStateMachine();
        }

        private void Update()
        {
            if(!IsStateHaveFixedUpdateMethods)
                _stateMachine.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            if(IsStateHaveFixedUpdateMethods)
                _stateMachine.CurrentState.Update();
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag("Car"))
            {
                IsEnemyCollisionCar = true;
            }
        }
        
        private void OnCollisionExit(Collision other)
        {
            if(other.gameObject.CompareTag("Car"))
            {
                IsEnemyCollisionCar = false;
            }
        }

        public void SetTarget(Car car, IDamageable carDamageableComponent)
        {
            _targetCar = car;
            _targedDamageableComponent = carDamageableComponent;
        }
        
        public void GetDamage(float damage)
        {
            StartCoroutine(HitRoutine());
            _health -= damage;
            if (_health <= 0)
            {
                Exploise();
            }
        }

        public void Init(Transform spawnPoint)
        {
            transform.position = spawnPoint.position;
            _stateMachine.ChangeState(_idleState);
        }

        public void Exploise() => StartCoroutine(ExploiseRoutine());
        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();
            
            _idleState = new IdleEnemyState(_stateMachine, this);
            _aggressiveState = new AggressiveEnemyState(_stateMachine, this);
            
            _stateMachine.Initialize(_idleState);
        }
        

        private IEnumerator ExploiseRoutine()
        {
            _isExploiseProcess = true;
            _stateMachine.ChangeState(_idleState);
            
            _stickmanGameObject.gameObject.SetActive(false);
            _exploiseParticle.Play();
            yield return new WaitForSeconds(_exploiseParticle.main.duration);
            Destroy(gameObject);
        }
        
        private IEnumerator HitRoutine()
        {
            _animator.SetBool(HIT_PARAMETER_NAME, false);
            _isHitProcess = true;
            _stateMachine.ChangeState(_idleState);
            
            _animator.SetBool(HIT_PARAMETER_NAME, true);
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);
            _isHitProcess = false;
            _animator.SetBool(HIT_PARAMETER_NAME, false);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TurretBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] protected float _speed;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _lifeTime;
    
    public event Action OnDisable;
    
    private float _savedSpeed;
    
    private float _senderForce;
    private float _senderDamage;
    private Vector3 _shootVector;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _savedSpeed = _speed;
    }
    
    private void OnEnable()
    {
        _speed = _savedSpeed;
        StartCoroutine(DisableRoutine());
    }
    
    private void FixedUpdate()
    {
        Fly();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("Car"))
            return;
        
        IDamageable damageableObject;
        other.gameObject.TryGetComponent<IDamageable>(out damageableObject);
            
        if (damageableObject != null)
            GiveDamage(damageableObject);

        Disable();
    }
    
    public void ApplySettings(float senderForce, Transform spawnPoint, Vector3 shootVector, float senderDamage = 0)
    {
        transform.position = spawnPoint.position;
        _senderForce = senderForce;
        _senderDamage = senderDamage;
        _shootVector = shootVector;
    }

    private void Fly()
    {
        _rigidbody.velocity = _shootVector  * _speed * _senderForce * Time.fixedDeltaTime;
    }

    private IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        OnDisable?.Invoke();
        gameObject.SetActive(false);
    }

    private void GiveDamage(IDamageable damageableObject)
    {
        damageableObject.GetDamage(_damage);

        gameObject.SetActive(false);
    }

    private void Disable()
    {
        OnDisable?.Invoke();
        gameObject.SetActive(false);
    }
}
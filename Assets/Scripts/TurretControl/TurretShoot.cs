using System.Collections;
using UnityEngine;
using Zenject;

namespace Datas
{
    public class TurretShoot : MonoBehaviour
    {
        [SerializeField] private TurretBullet turretBulletPrefab;
        [SerializeField] private int _poolSize;
        private ObjectPooler<TurretBullet> _bulletPooler;

        [SerializeField] private float _force;
        [SerializeField] private float _damage;
        [SerializeField] private float _shootDelay;
        private Vector2 _shootVector;
        
        private bool _isDelayActive;
        private bool _isCanShoot;
        
        private float _delayTimer;
        
        public void Awake()
        {
            _bulletPooler = new ObjectPooler<TurretBullet>(_poolSize);
            _bulletPooler.Init(turretBulletPrefab, null);
        }
        
        public void BlockShoot()
        {
            _isCanShoot = false;
        }
        
        public void UnblockShoot()
        {
            _isCanShoot = true;
        }
        
        public void Shoot(Transform spawnPoint)
        {
            if(_isDelayActive || !_isCanShoot)
                return;
            
            TurretBullet turretBullet = _bulletPooler.GetObject();

            turretBullet.OnDisable += () => _bulletPooler.ReturnObject(turretBullet);
            
            Vector3 direction =  spawnPoint.forward;
            
            turretBullet.ApplySettings(_force, spawnPoint, 
                direction, _damage);

            _isDelayActive = true;

            StartCoroutine(DelayRoutine());
        }

        private IEnumerator DelayRoutine()
        {
            yield return new WaitForSeconds(_shootDelay);
            _isDelayActive = false;
        }
    }
}
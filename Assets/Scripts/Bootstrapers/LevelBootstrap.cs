using CameraControl;
using UnityEngine;
using Zenject;

namespace BootStrapers
{
    public class LevelBootstrap : MonoBehaviour
    {
        private DiContainer _container;

        private const string CAR_PREFAB_PATH = "Prefabs/Car";
        
        [SerializeField] private Transform CarSpawnPoint;
        
        [SerializeField] private FollowCamera _followCamera;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        public void Awake()
        {
            Transform carTransform = SpawnCar();

            _followCamera.Init(carTransform);
        }
        
        private Transform SpawnCar()
        {
            Transform carTransform = _container.InstantiatePrefabResource
                    (CAR_PREFAB_PATH, CarSpawnPoint.position, Quaternion.identity, null)
                .transform;
            return carTransform;
        }
    }
}
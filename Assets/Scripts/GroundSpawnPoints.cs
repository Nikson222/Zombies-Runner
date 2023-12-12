using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GroundSpawnPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        private Dictionary<Transform, bool> _returnedPoints = new Dictionary<Transform, bool>();
        
        public Transform GetRandomPoint()
        {
            int i = Random.Range(0, _spawnPoints.Length);
            
            if (_returnedPoints.ContainsKey(_spawnPoints[i]))
            {
                return GetRandomPoint();
            }
            else
            {
                _returnedPoints.Add(_spawnPoints[i], true);
                return _spawnPoints[i];
            }
        }        
    }
}
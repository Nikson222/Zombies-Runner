using UnityEngine;
using Zenject;

namespace Datas
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _viewingAnglee;
        
        private IRotationInput _rotationInput;
        
        public float RotationSpeed => _rotationSpeed;
        
        [Inject]
        public void Construct(IRotationInput rotationInput)
        {
            _rotationInput = rotationInput;
            _rotationInput.OnRotationInput += Rotate;
        }
        
        public void BlockRotation()
        {
            _rotationInput.OnRotationInput -= Rotate;
        }
        
        public void UnblockRotation()
        {
            _rotationInput.OnRotationInput += Rotate;
        }
        
        private void Rotate(float angle)
        {
            float rotationAngle = angle * _rotationSpeed * Time.deltaTime;
            float rotationY = transform.localEulerAngles.y + rotationAngle;
            float leftBorder = 360 - _viewingAnglee;
        
            if (rotationY > leftBorder || rotationY < _viewingAnglee)
                transform.Rotate(0, 0, rotationAngle);
        }
    }
}
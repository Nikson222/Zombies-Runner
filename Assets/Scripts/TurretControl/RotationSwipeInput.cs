using System;
using UnityEngine;
using Zenject;

namespace Datas
{
    public class RotationSwipeInput : IRotationInput, ITickable
    {
        private Vector2 startPos = Vector2.zero;

        public event Action<float> OnRotationInput;
    
        public void Tick()
        {
            HandleRotationInput();
        }
    
        private void HandleRotationInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
            
                if (touch.phase == TouchPhase.Moved)
                {
                    float rotationAngle = touch.deltaPosition.x;
                
                    OnRotationInput?.Invoke(rotationAngle);
                }
            }
        }
    }
}
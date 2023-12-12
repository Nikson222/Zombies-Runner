using System;

namespace Datas
{
    public interface IRotationInput
    {
        public event Action<float> OnRotationInput;
    }
}
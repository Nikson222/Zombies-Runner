using Datas;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind(typeof(ITickable), typeof(IRotationInput))
                .To<RotationSwipeInput>()
                .AsSingle();
            
            
            Application.targetFrameRate = 60;
        }
    }
}
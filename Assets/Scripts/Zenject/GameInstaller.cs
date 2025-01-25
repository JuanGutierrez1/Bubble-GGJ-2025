using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private ScreenShake screenShake;

    public override void InstallBindings()
    {
        Container.BindInstance(screenShake);
    }
}
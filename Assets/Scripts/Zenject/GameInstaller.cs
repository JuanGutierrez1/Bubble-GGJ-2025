using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private ScreenShake screenShake;
    [SerializeField]
    private FinishedGameContainer finishedGameContainer;

    public override void InstallBindings()
    {
        Container.BindInstance(screenShake);
        Container.BindInstance(finishedGameContainer);
        Container.BindInterfacesAndSelfTo<BubbleManager>().AsSingle();
    }
}
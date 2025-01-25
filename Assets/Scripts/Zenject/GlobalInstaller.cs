using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField]
    private LevelManager _levelManager;
    [SerializeField]
    private AudioManager _audioManager;

    public override void InstallBindings()
    {
        Container.BindInstance(_levelManager);
        Container.BindInstance(_audioManager);
        Container.Bind<PlayerInputActions>().AsSingle();
    }
}
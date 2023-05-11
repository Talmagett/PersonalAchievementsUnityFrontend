using UnityEngine;
using Zenject;
public class DIInjecter : MonoInstaller
{
    [SerializeField] private AuthorizationController _authorizationController;
    [SerializeField] private LoadingView _loadingView;
    public override void InstallBindings()
    {
        Container.Bind<AuthorizationController>().FromInstance(_authorizationController).AsSingle();
        Container.Bind<LoadingView>().FromInstance(_loadingView).AsSingle();
    }
}
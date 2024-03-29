﻿using UnityEngine;
using Zenject;
public class DIInjecter : MonoInstaller
{
    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private AuthController _authorizationController;
    [SerializeField] private LoadingView _loadingView;
    public override void InstallBindings()
    {
        Container.Bind<AuthController>().FromInstance(_authorizationController).AsSingle();
        Container.Bind<LoadingView>().FromInstance(_loadingView).AsSingle();
        Container.Bind<MainMenuController>().FromInstance(_mainMenuController).AsSingle();
    }
}
using System.Collections.Generic;
using FSM.State;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

public class CoreLifetimeScope : LifetimeScope
{
    [SerializeField] private LoadUIView loadUIView;
    [SerializeField] private MenuUIView menuUIView;
    [SerializeField] private SplashUIView splashUIView;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(loadUIView);
        builder.RegisterInstance(menuUIView);
        builder.RegisterInstance(splashUIView);

        builder.Register<StateFactory<StateType>>(Lifetime.Singleton);
        builder.Register<IStateController<StateType>, StateController<StateType>>(Lifetime.Singleton);
    
        builder.RegisterComponentInHierarchy<GameCore>();
    
        builder.Register<SplashState>(Lifetime.Singleton)
            .WithParameter(typeof(IStateController<StateType>), resolver => resolver.Resolve<IStateController<StateType>>())
            .WithParameter(typeof(SplashUIView), splashUIView);
    
        builder.Register<LoadState>(Lifetime.Singleton)
            .WithParameter(typeof(IStateController<StateType>), resolver => resolver.Resolve<IStateController<StateType>>())
            .WithParameter(typeof(LoadUIView), loadUIView);
    
        builder.Register<MenuState>(Lifetime.Singleton)
            .WithParameter(typeof(IStateController<StateType>), resolver => resolver.Resolve<IStateController<StateType>>())
            .WithParameter(typeof(MenuUIView), menuUIView);
    
        builder.RegisterBuildCallback(resolver =>
        {
            var loadState = resolver.Resolve<LoadState>();
            loadUIView.Construct(loadState);
            
            
            var factory = resolver.Resolve<StateFactory<StateType>>();
        
            factory.Initialize(new Dictionary<StateType, IState>
            {
                { StateType.Splash, resolver.Resolve<SplashState>() },
                { StateType.Load, resolver.Resolve<LoadState>() },
                { StateType.Menu, resolver.Resolve<MenuState>() }
            });
        });
    }
}

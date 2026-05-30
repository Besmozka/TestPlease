using System.Threading;
using Cysharp.Threading.Tasks;
using FSM.State;
using UI;
using UnityEngine;

public class SplashState : IState
{
    private int _waitTimer = 1000;
    
    private IStateController<StateType> _stateController;

    private SplashUIView _splashUIView;

    public SplashState(IStateController<StateType> stateController, SplashUIView splashUIView)
    {
        _stateController = stateController;

        _splashUIView = splashUIView;
    }
    
    public async UniTask EnterAsync(CancellationToken ct)
    {
        _splashUIView.ShowLogo(true);

        await UniTask.Delay(_waitTimer, cancellationToken: ct);

        _stateController.EnterStateAsync(StateType.Load, ct);
    }

    public UniTask ExitAsync(CancellationToken ctx)
    {
        return UniTask.CompletedTask;
    }
}

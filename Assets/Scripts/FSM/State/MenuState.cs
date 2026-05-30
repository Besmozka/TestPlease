using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FSM.State;
using R3;
using UnityEngine.UIElements;

public class MenuState : IState
{
    private IStateController<StateType> _stateController;
    private MenuUIView _menuView;
    
    private IDisposable _subsciber;
    
    public MenuState(IStateController<StateType> stateController, MenuUIView menuView)
    {
        _stateController = stateController;
        _menuView = menuView;
    }
    
    public UniTask EnterAsync(CancellationToken ct)
    {
        _menuView.ShowMainMenu(true);

        _subsciber = _menuView.RestartClicked
            .Subscribe(_ =>
            {
                RestartAsync(ct).Forget();
            });

        return UniTask.CompletedTask;
    }

    private async UniTask RestartAsync(CancellationToken ct)
    {
        await _stateController.EnterStateAsync(StateType.Load, ct);
    }

    public UniTask ExitAsync(CancellationToken ct)
    {
        _subsciber?.Dispose();
        _menuView.ShowMainMenu(false);
        return UniTask.CompletedTask;
    }
}

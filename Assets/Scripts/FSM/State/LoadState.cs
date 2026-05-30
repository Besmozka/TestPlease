using System.Threading;
using Cysharp.Threading.Tasks;
using FSM.State;
using R3;
using UI;
using UnityEngine;

public class LoadState : IState
{
    private ReactiveProperty<float> _progress = new (0);
    public ReadOnlyReactiveProperty<float> Progress => _progress;
    
    private IStateController<StateType> _stateController;

    private LoadUIView _loadUIView;

    public LoadState(IStateController<StateType> stateController, LoadUIView loadUIView)
    {
        _stateController = stateController;
        _loadUIView = loadUIView;
    }
    
    public async UniTask EnterAsync(CancellationToken ct)
    {
        _loadUIView.Show(true);

        _progress.Value = 0f;

        for (int i = 0; i < 5; i++)
        {
            await UniTask.Delay(200, cancellationToken: ct);

            _progress.Value += 0.2f;
        }

        _stateController.EnterStateAsync(StateType.Menu, ct);
    }

    public UniTask ExitAsync(CancellationToken ctx)
    {
        _loadUIView.Show(false);

        return UniTask.CompletedTask;
    }
}

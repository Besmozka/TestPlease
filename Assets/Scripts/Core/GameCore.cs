using System;
using System.Threading;
using FSM.State;
using UnityEngine;
using VContainer;

public class GameCore : MonoBehaviour
{
    private IStateController<StateType> _stateController;
    private CancellationTokenSource _ct;
    
    [Inject]
    public void Init(IStateController<StateType> stateController)
    {
        _stateController = stateController;
    }

    private void Start()
    {
        _ct = new CancellationTokenSource();
        
        _stateController.EnterStateAsync(StateType.Splash, _ct.Token);
    }

    private void OnDestroy()
    {
        _ct.Cancel();
        _ct.Dispose();
    }
}

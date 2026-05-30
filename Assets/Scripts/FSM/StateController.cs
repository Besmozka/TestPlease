using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using FSM.State;
using UnityEngine;

public class StateController<T> : IStateController<T> where T : Enum
{
    private readonly StateFactory<T> _factory;
    private IState _currentState;

    public StateController(StateFactory<T> factory)
    {
        _factory = factory;
    }
    
    public async UniTask EnterStateAsync(T stateType, CancellationToken ct)
    {
        var next = _factory.Get(stateType);

        if (_currentState != null)
        {
            await _currentState.ExitAsync(ct);
        }

        _currentState = next;

        await _currentState.EnterAsync(ct);
    }
}

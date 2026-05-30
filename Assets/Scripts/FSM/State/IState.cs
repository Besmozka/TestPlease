using System.Threading;
using Cysharp.Threading.Tasks;
using FSM.State;
using UnityEngine;

public interface IState
{
    public UniTask EnterAsync(CancellationToken ct);
    public UniTask ExitAsync(CancellationToken ctx);
}

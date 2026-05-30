using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IStateController<T>
{
    public UniTask EnterStateAsync(T stateType, CancellationToken ctx);
}

using System;
using System.Collections.Generic;

public class StateFactory<T> where T : Enum
{
    private Dictionary<T, IState> _map;

    public void Initialize(Dictionary<T, IState> map)
    {
        _map = map;
    }

    public IState Get(T state)
    {
        return _map[state];
    }
}
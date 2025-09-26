using System;
using System.Collections.Generic;

public class ValueModifier<T>
{
    private readonly List<Func<T, T>> _operations = new();

    public void Add(Func<T, T> operation) => _operations.Add(operation);
    public void Remove(Func<T, T> modifier) => _operations.Remove(modifier);
    public void Clear() => _operations.Clear();

    public T Result(T baseValue)
    {
        T result = baseValue;

        foreach (var oper in _operations)
            result = oper(result);

        return result;
    }
}
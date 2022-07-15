using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public interface IValue<T> : IEqualityComparer<IValue<T>>, System.IEquatable<IValue<T>>
    {
        T Value { get; }
    }
}

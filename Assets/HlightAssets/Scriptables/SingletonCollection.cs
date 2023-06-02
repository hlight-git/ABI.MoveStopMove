using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCollection<SelfT, KeyT, ValueT> : SingletonScriptableObject<SelfT>
    where SelfT : SingletonCollection<SelfT, KeyT, ValueT>
    where KeyT : System.Enum
{
    [SerializeField] protected int firstEnumValue;
    [SerializeField] protected List<ValueT> values;
    public virtual ValueT Get(KeyT key)
    {
        return values[(int)(object)key - firstEnumValue];
    }
}

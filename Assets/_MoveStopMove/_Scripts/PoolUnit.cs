using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolUnit<TF> : MonoBehaviour
{
    private TF tf;

    [Header("- Pool Unit:")]
    public PoolType PoolType;

    public TF UnitTF
    {
        get
        {
            if (tf == null)
            {
                OnFirstGetUnitTF();
            }
            return tf;
        }
        protected set => tf = value;
    }

    protected abstract void OnFirstGetUnitTF();
}

public class UIUnit : PoolUnit<RectTransform>
{
    protected override void OnFirstGetUnitTF() => UnitTF = GetComponent<RectTransform>();
}
public class TmpGameUnit : PoolUnit<Transform>
{
    protected override void OnFirstGetUnitTF() => UnitTF = transform;
}
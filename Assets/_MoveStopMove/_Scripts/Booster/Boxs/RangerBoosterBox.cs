using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerBoosterBox : AbBoosterBox<IRanger>
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.Tag.CHARACTER))
        {
            OnCollected(RangerCache.Get(other));
        }
    }
}
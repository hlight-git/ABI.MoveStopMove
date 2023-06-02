using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resize", menuName = "Scriptable Objects/Booster/Ranger/Resize")]
public class ResizeRangerBoosterData : AbBoosterData<IRanger, ResizeRangerBooster> { }

[System.Serializable]
public class ResizeRangerBooster : AbOnRangerBooster
{
    [SerializeField] bool isBigger;
    [SerializeField] float duration;
    float backUpSize;
    float effectValue;

    public override void OnTriggered()
    {
        backUpSize = Target.Size;
        effectValue = isBigger? Constant.Ranger.MAX_SIZE : Constant.Ranger.MIN_SIZE;
        Target.SetSize(effectValue);
    }

    public override void OnExecute()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            OnFinish();
        }
    }

    public override void OnCancel()
    {
        float offset = Target.Size - effectValue;
        Target.SetSize(backUpSize + offset);
    }
}
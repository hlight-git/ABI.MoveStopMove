//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BotController : MonoBehaviour
//{
//    ScheduledInvoker invoker;
//    public Queue<Vector3> Actions { get; private set; }

//    private void Awake()
//    {
//        Actions = new Queue<Vector3>();
//        invoker = new ScheduledInvoker();
//    }
//    void Update()
//    {
//        if (invoker.Countdown())
//        {
//            if (Actions.Count > 0)
//            {
//                //invoker.Schedule()
//            }
//        }
        
//    }
//    void DecideToChangeTheFollowTargetTo(AbsCharacter enemy)
//    {
//        if (Random.value < 0.5)
//        {
//            return;
//        }

//    }
//    void FollowTarget()
//    {

//    }

//    void Stop()
//    {

//    }

//    void Dodge()
//    {

//    }
//}



//[System.Serializable]
//abstract class BotAbility
//{
//    [Range(0, 1)]
//    protected float proficiency;
//    public BotAbility()
//    {
//        proficiency = Random.value;
//    }
//}

//class CanDodge : BotAbility
//{
//    void RunAway()
//    {

//    }
//    void ContinueAttack()
//    {

//    }
//}

//class CanSelectGoodOpponent
//{

//}
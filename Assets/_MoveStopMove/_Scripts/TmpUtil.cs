using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TmpUtil : MonoBehaviour
{
    public static LayerMask LayerOf(string layerName) => 1 << LayerMask.NameToLayer(layerName);
    public static int RandomSign() => RandomBool() ? -1 : 1;
    public static bool RandomBool(float truePossibility = 0.5f) => Random.value <= truePossibility;
    public static T Choice<T>(List<T> list) => list[Random.Range(0, list.Count)];
    public static T Choice<T>(List<T> list, params T[] excepts)
    {
        List<T> exceptVals = new List<T>(excepts);
        List<int> choices = new List<int>(Enumerable.Range(0, list.Count - 1));
        int choice = Choice(choices);
        while (exceptVals.Contains(list[choice])) {
            choices.Remove(choice);
            choice = Choice(choices);
        }
        return list[choice];
    }
    public static bool IsInRange(float value, Vector2 range, bool includeMin = true, bool includeMax = true)
    {
        if (includeMin && includeMax)
        {
            return range.x <= value && value <= range.y;
        }
        if (includeMin)
        {
            return range.x <= value && value < range.y;
        }
        if (includeMax)
        {
            return range.x <= value && value <= range.y;
        }
        return range.x < value && value < range.y;
    }

    public static int EnumSize<E>() => System.Enum.GetNames(typeof(E)).Length;
}

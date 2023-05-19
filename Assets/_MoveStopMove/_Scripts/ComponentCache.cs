using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpCache
{
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
}
public class TmpCache<T>
{
    private static readonly Dictionary<Collider, T> cacheDict = new Dictionary<Collider, T>();
    public static T Get(Collider collider)
    {
        if (!cacheDict.ContainsKey(collider))
        {
            cacheDict.Add(collider, collider.GetComponent<T>());
        }
        return cacheDict[collider];
    }
}
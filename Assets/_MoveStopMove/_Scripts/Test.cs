using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
public class Test : MonoBehaviour, ITest
{
    ITest x;

    public string Name => "tqh";

    private void Start()
    {
        x = GetComponent<ITest>();
        print(x.Name);
    }
}

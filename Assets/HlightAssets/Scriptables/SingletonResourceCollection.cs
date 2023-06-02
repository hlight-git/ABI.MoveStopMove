using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SingletonResourceCollection<SelfT, KeyT, ValueT> : SingletonCollection<SelfT, KeyT, ValueT>, IDataStore
    where SelfT : SingletonResourceCollection<SelfT, KeyT, ValueT>
    where KeyT : System.Enum
    where ValueT : Object
{
#if UNITY_EDITOR
    [Header("Editor:")]
    [SerializeField] string dataPath;
    [SerializeField, TextArea] string EnumValues;
#endif
    public void LoadData()
    {
        ValueT[] data = Resources.LoadAll<ValueT>(dataPath);
        if (data.Length == 0)
        {
            Debug.Log("Not found data at current `dataPath`.");
            return;
        }
        values = new List<ValueT>(data);
        EnumValues = "";
        for (int i = 0; i < data.Length; i++)
        {
            EnumValues += $"{data[i].name} = {i + firstEnumValue},\n";
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(SingletonResourceCollection<,,>), true)]
public class SingletonResourceCollectionEditor : DataStoreEditor { }
#endif
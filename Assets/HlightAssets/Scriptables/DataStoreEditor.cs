using UnityEditor;
using UnityEngine;

public class DataStoreEditor : Editor
{
    protected IDataStore dataStore;
    private void OnEnable()
    {
        dataStore = (IDataStore)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Load Data"))
        {
            dataStore.LoadData();
            EditorUtility.SetDirty((Object)dataStore);
        }
    }
}

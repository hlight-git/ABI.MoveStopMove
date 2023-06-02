using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "Scriptable Objects/User Data")]
public class UserData : SingletonScriptableObject<UserData>, IDataStore
{
    public const string KEY_LEVEL = "Level";
    public const string KEY_COIN = "Coin";
    public const string KEY_SOUNDISON = "SoundIsOn";
    public const string KEY_VIBRATE = "VibrateIsOn";
    public const string KEY_REMOVEADS = "RemoveAds";
    public const string KEY_FIRSTLOAD = "FirstLoad";

    public const string KEY_PLAYER_NAME = "PlayerName";
    public const string KEY_PLAYER_WEAPON = "PlayerWeapon";
    public const string KEY_PLAYER_HAT = "PlayerHat";
    public const string KEY_PLAYER_PANT = "PlayerPant";
    public const string KEY_PLAYER_ACCESSORY = "PlayerAccessory";
    public const string KEY_PLAYER_SKIN = "PlayerSkin";
    public const string KEY_PLAYER_COLOR = "PlayerColor";

    public const WeaponType DEFAULT_WEAPON = WeaponType.W_Hammer_1;
    public const HatType DEFAULT_HAT = HatType.HAT_None;
    public const PantType DEFAULT_PANT = PantType.Pant_1;
    public const AccessoryType DEFAULT_ACCESSORY = AccessoryType.ACC_None;
    public const SkinType DEFAULT_SKIN = SkinType.SKIN_Normal;
    public const UnitColor DEFAULT_COLOR = UnitColor.Cyan;

    public bool IsFirstLoad;
    public bool SoundIsOn;
    public bool Vibrate;
    public bool RemoveAds;

    public string Name;
    public int Level;
    public int Coin;

    public WeaponType Weapon;
    public HatType Hat;
    public PantType Pant;
    public AccessoryType Accessory;
    public SkinType Skin;
    public UnitColor Color;

    //Example
    // UserData.Ins.SetInt(UserData.Key_Level, ref UserData.Ins.level, 1);

    //data for list
    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public void SetDataState(string key, int ID, int state)
    {
        PlayerPrefs.SetInt(key + ID, state);
    }
    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public int GetDataState(string key, int ID, int state = 0)
    {
        return PlayerPrefs.GetInt(key + ID, state);
    }

    public void SetDataState(string key, int state)
    {
        PlayerPrefs.SetInt(key, state);
    }

    public int GetDataState(string key, int state = 0)
    {
        return PlayerPrefs.GetInt(key, state);
    }

    /// <summary>
    /// Key_Name
    /// if(bool) true == 1
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetIntData(string key, ref int variable, int value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value);
    }

    public void SetBoolData(string key, ref bool variable, bool value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public void SetFloatData(string key, ref float variable, float value)
    {
        variable = value;
        PlayerPrefs.GetFloat(key, value);
    }

    public void SetStringData(string key, ref string variable, string value)
    {
        variable = value;
        PlayerPrefs.SetString(key, value);
    }

    public void SetEnumData<T>(string key, ref T variable, T value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, System.Convert.ToInt32(value));
    }

    public void SetEnumData<T>(string key, T value)
    {
        PlayerPrefs.SetInt(key, System.Convert.ToInt32(value));
    }

    public T GetEnumData<T>(string key, T defaultValue) where T : System.Enum
    {
        return (T)System.Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key, System.Convert.ToInt32(defaultValue)));
    }


#if UNITY_EDITOR
    [Space(10)]
    [Header("---- Editor ----")]
    public bool TestMode;
#endif

    public void LoadData()
    {

#if UNITY_EDITOR
        if (TestMode) return;
#endif
        IsFirstLoad = PlayerPrefs.GetInt(KEY_FIRSTLOAD, 1) == 1;
        Level = PlayerPrefs.GetInt(KEY_LEVEL, 0);
        Coin = PlayerPrefs.GetInt(KEY_COIN, 0);
        Name = PlayerPrefs.GetString(KEY_PLAYER_NAME, "You");

        SoundIsOn = PlayerPrefs.GetInt(KEY_SOUNDISON, 1) == 1;
        Vibrate = PlayerPrefs.GetInt(KEY_VIBRATE, 1) == 1;
        RemoveAds = PlayerPrefs.GetInt(KEY_REMOVEADS, 0) == 1;

        Weapon = GetEnumData(KEY_PLAYER_WEAPON, DEFAULT_WEAPON);
        Hat = GetEnumData(KEY_PLAYER_HAT, DEFAULT_HAT);
        Pant = GetEnumData(KEY_PLAYER_PANT, DEFAULT_PANT);
        Accessory = GetEnumData(KEY_PLAYER_ACCESSORY, DEFAULT_ACCESSORY);
        Skin = GetEnumData(KEY_PLAYER_SKIN, DEFAULT_SKIN);
        Color = GetEnumData(KEY_PLAYER_COLOR, DEFAULT_COLOR);
    }

    public void OnResetData()
    {
        PlayerPrefs.DeleteAll();
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString(KEY_PLAYER_NAME, Name);
        PlayerPrefs.SetInt(KEY_LEVEL, Level);
        PlayerPrefs.SetInt(KEY_COIN, Coin);

        SetBoolData(KEY_SOUNDISON, ref SoundIsOn, SoundIsOn);
        SetBoolData(KEY_VIBRATE, ref Vibrate, Vibrate);
        SetBoolData(KEY_REMOVEADS, ref RemoveAds, RemoveAds);
        SetBoolData(KEY_FIRSTLOAD, ref IsFirstLoad, IsFirstLoad);

        SetEnumData(KEY_PLAYER_WEAPON, Weapon);
        SetEnumData(KEY_PLAYER_HAT, Hat);
        SetEnumData(KEY_PLAYER_PANT, Pant);
        SetEnumData(KEY_PLAYER_ACCESSORY, Accessory);
        SetEnumData(KEY_PLAYER_SKIN, Skin);
        SetEnumData(KEY_PLAYER_COLOR, Color);
    }

}



#if UNITY_EDITOR
[CustomEditor(typeof(UserData))]
public class TmpUserDataEditor : DataStoreEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Clear Data"))
        {
            (dataStore as UserData).OnResetData();
            EditorUtility.SetDirty((Object)dataStore);
        }
        if (GUILayout.Button("Save Data"))
        {
            (dataStore as UserData).SaveData();
            EditorUtility.SetDirty((Object)dataStore);
        }
    }
}
#endif
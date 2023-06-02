using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ShopData", menuName = "Scriptable Objects/Shop Data", order = 1)]
public class ShopData : ScriptableObject, IDataStore
{
    public ShopItemResources<HatType> Hats;
    public ShopItemResources<PantType> Pants;
    public ShopItemResources<AccessoryType> Accessories;
    public ShopItemResources<SkinType> Skins;

    public void LoadData()
    {
        LoadData(Hats);
        LoadData(Pants, false);
        LoadData(Accessories);
        LoadData(Skins);
    }
    public void LoadData<T>(ShopItemResources<T> shopItemResources, bool hasNone = true) where T : System.Enum
    {
        shopItemResources.Clear();
        if (hasNone)
        {
            shopItemResources.Add(new ShopItemData<T>(null));
        }
        Sprite[] resources = Resources.LoadAll<Sprite>(shopItemResources.dataPath);
        for (int i = 0; i < resources.Length; i++)
        {
            shopItemResources.Add(new ShopItemData<T>(resources[i]));
        }
    }
}
[CustomEditor(typeof(ShopData))]
public class ShopDataEditor : DataStoreEditor { }

[System.Serializable]
public class ShopItemResources<T> where T: System.Enum
{
    public string dataPath;
    [SerializeField] List<ShopItemData<T>> items;
    public List<ShopItemData<T>> Items => items;
    public void Add(ShopItemData<T> item)
    {
        items.Add(item);
    }
    public void Clear()
    {
        items.Clear();
    }
}

[System.Serializable]
public class ShopItemData<T> : ShopItemData where T : System.Enum
{
    public T type;

    public ShopItemData(Sprite icon) : base(icon)
    {
    }
}

public class ShopItemData
{
    public Sprite icon;
    public int cost;
    public int ads;
    public ShopItemData(Sprite icon)
    {
        this.icon = icon;
        cost = 100;
        ads = 1;
    }
}
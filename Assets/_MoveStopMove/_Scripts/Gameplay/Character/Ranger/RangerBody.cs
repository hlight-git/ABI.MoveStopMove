using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerBody : Body
{
    [Header("Body parts")]
    [SerializeField] Transform head;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    [SerializeField] Renderer pant;

    GameUnit hat;
    GameUnit accessory;

    public Weapon Weapon { get; private set; }
    #region Change Equipments
    public void ChangeWeapon(WeaponType weaponType)
    {
        Weapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, rightHand);
    }

    public void ChangeAccessory(AccessoryType accessoryType)
    {
        if (IsntBodySet && accessoryType != AccessoryType.ACC_None)
        {
            accessory = SimplePool.Spawn<GameUnit>((PoolType)accessoryType, leftHand);
        }
    }

    public void ChangeHat(HatType hatType)
    {
        if (IsntBodySet && hatType != HatType.HAT_None)
        {
            hat = SimplePool.Spawn<GameUnit>((PoolType)hatType, head);
        }
    }

    public void ChangePant(PantType pantType)
    {
        if (IsntBodySet)
        {
            pant.material = PantData.Ins.Get(pantType);
        }
    }
    #endregion

    #region Despawn
    public override void OnDespawn()
    {
        DespawnWeapon();
        DespawnHat();
        DespawnAccessory();
        base.OnDespawn();
    }
    public void DespawnHat()
    {
        if (hat) SimplePool.Despawn(hat);
    }
    public void DespawnAccessory()
    {
        if (accessory) SimplePool.Despawn(accessory);
    }

    public void DespawnWeapon()
    {
        if (Weapon) SimplePool.Despawn(Weapon);
    }
    #endregion
}
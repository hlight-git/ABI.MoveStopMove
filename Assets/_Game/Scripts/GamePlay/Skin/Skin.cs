using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : GameUnit
{
    [SerializeField] PantData pantData;
    [SerializeField] Animator animator;

    [Header("Body parts")]
    [SerializeField] Transform head;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;
    [SerializeField] Renderer pant;

    [SerializeField] bool canChangeOutfits = false;

    Hat hat;
    Accessory accessory;
    public Weapon Weapon { get; private set; }

    public AnimPlayer<CharacterAnimation> AnimPlayer { get; private set; }

    void Awake() => AnimPlayer = new AnimPlayer<CharacterAnimation>(animator, CharacterAnimation.idle);

    #region Change Equipments
    public void ChangeWeapon(WeaponType weaponType)
    {
        Weapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, rightHand);
    }

    public void ChangeAccessory(AccessoryType accessoryType)
    {
        if (canChangeOutfits && accessoryType != AccessoryType.ACC_None)
        {
            accessory = SimplePool.Spawn<Accessory>((PoolType)accessoryType, leftHand);
        }
    }

    public void ChangeHat(HatType hatType)
    {
        if (canChangeOutfits && hatType != HatType.HAT_None)
        {
            hat = SimplePool.Spawn<Hat>((PoolType)hatType, head);
        }
    }

    public void ChangePant(PantType pantType)
    {
        if (canChangeOutfits)
        {
            pant.material = pantData.GetPant(pantType);
        }
    }
    #endregion

    #region Despawn
    public void OnDespawn()
    {
        DespawnWeapon();
        DespawnHat();
        DespawnAccessory();
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

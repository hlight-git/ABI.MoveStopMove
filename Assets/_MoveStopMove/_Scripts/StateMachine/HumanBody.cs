using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBody : Body<CharacterAnimation>
{
    [SerializeField] PantData pantData;
    [SerializeField] Animator animator;

    [Header("Body parts")]
    [SerializeField] Transform head;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;
    [SerializeField] Renderer pant;

    [SerializeField] bool canChangeOutfits = false;

    GameUnit hat;
    GameUnit accessory;
    public Weapon Weapon { get; private set; }

    protected override void Awake() => OnInit(CharacterAnimation.idle);

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
    public override void OnDespawn()
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
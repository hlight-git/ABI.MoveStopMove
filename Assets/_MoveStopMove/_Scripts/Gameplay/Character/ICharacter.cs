using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICharacter : IResizable, IBulletHitable
{
    Transform TF { get; }
    bool IsDead { get; }
    public int HealthPoint { get; }
    public UnitColor Color { get; }
    UnityAction<ICharacter> OnDeathEvents { get; set; }
    void OnDeath();
    void SetIndicatorAlpha(float alpha);
}

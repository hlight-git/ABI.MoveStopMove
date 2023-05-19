using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChar
{
    Transform UnitTF { get; }
    void SetSize(float size);
    void OnDeath();
    void OnHit();
}

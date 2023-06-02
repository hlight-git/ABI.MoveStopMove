using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : GameUnit
{
    [SerializeField] Renderer bodyRenderer;
    [SerializeField] Animator animator;
    [SerializeField] bool isntBodySet;
    public bool IsntBodySet => isntBodySet;
    public Animator Animator => animator;
    public void ChangeMaterial(Material material)
    {
        bodyRenderer.material = material;
    }
    public virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
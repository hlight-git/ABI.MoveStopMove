using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbBoosterBox<CollectorT> : GameUnit
{
    [SerializeField] BoosterCollection<CollectorT> boosterCollection;
    [SerializeField] Renderer unitRenderer;
    [SerializeField] float rotateSpeed = 100;
    AbBoosterData<CollectorT> boosterData;
    private void Update()
    {
        TF.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
    }
    public virtual void OnSpawn()
    {
        boosterData = boosterCollection.GetRandomBooster();
        unitRenderer.material = boosterData.Material;
    }
    public virtual void OnCollected(CollectorT collector)
    {
        boosterData.Boost(collector);
        SimplePool.Despawn(this);
    }
    protected abstract void OnTriggerEnter(Collider other);
}
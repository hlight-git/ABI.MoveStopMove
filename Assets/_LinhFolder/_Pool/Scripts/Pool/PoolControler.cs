using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PoolControler : MonoBehaviour
{
    [Header("---- POOL CONTROLER TO INIT POOL ----")]
    [Header("Put object pool to list Pool or Resources/Pool")]
    [Header("Preload: Init Poll")]
    [Header("Spawn: Take object from pool")]
    [Header("Despawn: return object to pool")]
    [Header("Collect: return objects type to pool")]
    [Header("CollectAll: return all objects to pool")]

    [Space]
    [Header("Pool")]
    public List<PoolAmount> Pool;

    [Header("Particle")]
    public ParticleAmount[] Particle;


    public void Awake()
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            SimplePool.Preload(Pool[i].prefab, Pool[i].amount, Pool[i].root, Pool[i].collect);
        }

        for (int i = 0; i < Particle.Length; i++)
        {
            ParticlePool.Preload(Particle[i].prefab, Particle[i].amount, Particle[i].root);
            ParticlePool.Shortcut(Particle[i].particleType, Particle[i].prefab);
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(PoolControler))]
public class PoolControlerEditor : Editor
{
    PoolControler pool;

    private void OnEnable()
    {
        pool = (PoolControler)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Quick Root"))
        {
            for (int i = 0; i < pool.Pool.Count; i++)
            {
                if (pool.Pool[i].root == null)
                {
                    Transform tf = new GameObject(pool.Pool[i].prefab.poolType.ToString()).transform;
                    tf.parent = pool.transform;
                    pool.Pool[i].root = tf; 
                }
            }
            
            for (int i = 0; i < pool.Particle.Length; i++)
            {
                if (pool.Particle[i].root == null)
                {
                    Transform tf = new GameObject(pool.Particle[i].particleType.ToString()).transform;
                    tf.parent = pool.transform;
                    pool.Particle[i].root = tf; 
                }
            }
        }

        if (GUILayout.Button("Get Prefab Resource"))
        {
            GameUnit[] resources = Resources.LoadAll<GameUnit>("Pool");

            for (int i = 0; i < resources.Length; i++)
            {
                bool isDuplicate = false;
                for (int j = 0; j < pool.Pool.Count; j++)
                {
                    if (resources[i].poolType == pool.Pool[j].prefab.poolType)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    Transform root = new GameObject(resources[i].name).transform;

                    PoolAmount newPool = new PoolAmount(root, resources[i], SimplePool.DEFAULT_POOL_SIZE, true);

                    pool.Pool.Add(newPool);
                }
            }
        }
    }
}

#endif

[System.Serializable]
public class PoolAmount
{
    [Header("-- Pool Amount --")]
    public Transform root;
    public GameUnit prefab;
    public int amount;
    public bool collect;

    public PoolAmount (Transform root, GameUnit prefab, int amount, bool collect)
    {
        this.root = root;
        this.prefab = prefab;
        this.amount = amount;
        this.collect = collect;
    }
}


[System.Serializable]
public class ParticleAmount
{
    public Transform root;
    public ParticleType particleType;
    public ParticleSystem prefab;
    public int amount;
}


public enum ParticleType
{
    Hit_1,
    Hit_2,
    Hit_3,

    LevelUp_1,
    LevelUp_2,
    LevelUp_3,
}

public enum PoolType
{
    None = 0,

    W_Hammer_1 = 1001,
    W_Hammer_2 = 1002,
    W_Hammer_3 = 1003,
    W_Candy_1 = 1004,
    W_Candy_2 = 1005,
    W_Candy_3 = 1006,
    W_Boomerang_1 = 1007,
    W_Boomerang_2 = 1008,
    W_Boomerang_3 = 1009,

    B_Hammer_1 = 2001,
    B_Hammer_2 = 2002,
    B_Hammer_3 = 2003,
    B_Candy_1 = 2004,
    B_Candy_2 = 2005,
    B_Candy_3 = 2006,
    B_Boomerang_1 = 2007,
    B_Boomerang_2 = 2008,
    B_Boomerang_3 = 2009,

    SKIN_Normal = 3001,
    SKIN_Devil = 3002,
    SKIN_Angle = 3003,
    SKIN_Witch = 3004,
    SKIN_Deadpool = 3005,
    SKIN_Thor = 3006,
    SKIN_Zombie = 3007,

    HAT_Arrow = 4001,
    HAT_Cap = 4002,
    HAT_Cowboy = 4003,
    HAT_Crown = 4004,
    HAT_Ear = 4005,
    HAT_StrawHat = 4006,
    HAT_Headphone = 4007,
    HAT_Horn = 4008,
    HAT_Police = 4009,

    ACC_Book = 5002,
    ACC_Captain = 5003,
    ACC_Headphone = 5004,
    ACC_Shield = 5005,

    Bot = 100,
    Zombie = 101,
    BulletPath = 200,
    RangerBoosterBox = 201,
    AudioSource = 202,
    UI_TargetIndicator = 10001,
    UI_HumanIndicator = 10002,
    UI_CombatText = 10003,
}



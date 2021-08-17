using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolConfig<T, U>
    where T : System.Enum
    where U : MonoBehaviour
{
    public T m_Type;
    public U m_Prefab;
    public int m_InitialSize;
}

[System.Serializable]
public abstract class PoolFamily<T,U>: MonoBehaviour
    where T : System.Enum
    where U : MonoBehaviour
{
    public static PoolFamily<T, U> m_Instance;

    /// <summary>
    /// Store the pools for the family
    /// </summary>
    protected Dictionary<T, List<U>> m_Pools;
    protected Dictionary<T, List<U>> m_InUsedPools;

    /// <summary>
    /// Store the prefabs for each family member
    /// </summary>
    protected List<PoolConfig<T, U>> m_PoolConfigs;

    protected virtual void SetUp()
    {
        m_Pools = new Dictionary<T, List<U>>();
        m_InUsedPools = new Dictionary<T, List<U>>();

        if (m_PoolConfigs != null)
        {
            for (int configIndex = 0; configIndex < m_PoolConfigs.Count; configIndex++)
            {
                PoolConfig<T, U> poolConfig = m_PoolConfigs[configIndex];

                m_Pools[poolConfig.m_Type] = new List<U>();
                m_InUsedPools[poolConfig.m_Type] = new List<U>();

                for (int i = 0; i < poolConfig.m_InitialSize; i++)
                {
                    U instance = Instantiate(poolConfig.m_Prefab);

                    ReturnObjectToPool(poolConfig.m_Type, instance);
                }
            }
        }
    }

    protected virtual void Start()
    {
        if(m_Instance != null)
        {
            Debug.LogError("Another instance of this pool has existed");
            Destroy(this);
        }
        else
        {
            m_Instance = this;
            SetUp();
        }
    }

    protected virtual void HandleNotEnoughObject(T type)
    {
        PoolConfig<T, U> poolConfig = m_PoolConfigs.Find(x => x.m_Type.Equals(type));
        List<U> pool = m_Pools[type];
        for (int i = 0; i < poolConfig.m_InitialSize; i++)
        {
            var instance = Instantiate(poolConfig.m_Prefab);

            pool.Add(instance);
        }

        Debug.Log("Handled not enought object in pool");
    }

    public U GetObject(T type)
    {
        List<U> pool = m_Pools[type];
        int currentNumberInPool = pool.Count;

        if(currentNumberInPool <= 0)
        {
            HandleNotEnoughObject(type);
        }

        // If after handle not enough object, but we still dont have object, then return null
        currentNumberInPool = pool.Count;
        if(currentNumberInPool <= 0)
        {
            return null;
        }

        U instance = pool[currentNumberInPool - 1];
        
        pool.RemoveAt(currentNumberInPool - 1);

        m_InUsedPools[type].Add(instance);

        instance.transform.parent = null;
        instance.gameObject.SetActive(true);

        return instance;
    }

    public void ReturnObjectToPool(T type, U instance)
    {
        List<U> pool = m_Pools[type];

        instance.gameObject.SetActive(false);
        instance.transform.parent = transform;
        pool.Add(instance);

        m_InUsedPools[type].Remove(instance);
    }
}

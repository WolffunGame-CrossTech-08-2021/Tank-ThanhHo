using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectedArea : MonoBehaviour
{
    [SerializeField] float m_AreaRadius;
    [SerializeField] float m_AreaDuration;
    [SerializeField] Effect m_EffectPrototype;

    //public PoisonEffect m_PoisonEffectPrefab;

    List<TankInfo> m_TanksInArea;

    TankInfo m_Owner;

    public void SetUp(TankInfo owner,float radius, float duration, Effect effectPrototype)
    {
        m_Owner = owner;

        m_AreaRadius = radius;
        m_AreaDuration = duration;

        m_EffectPrototype = effectPrototype;
        m_EffectPrototype.transform.parent = transform;

        transform.localScale = new Vector3(m_AreaRadius, m_AreaRadius, m_AreaRadius);
        m_TanksInArea = new List<TankInfo>();
    }

    private void Start()
    {
        Destroy(gameObject, m_AreaDuration);
    }

    private void Update()
    {
        foreach (var tank in m_TanksInArea)
        {
            AddEffect(tank);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var tankInfo = other.GetComponent<TankInfo>();

            if (tankInfo == null) return;

            if (tankInfo == m_Owner) return;

            m_TanksInArea.Add(tankInfo);

            AddEffect(tankInfo);
        }
    }

    private void AddEffect(TankInfo tankInfo)
    {
        Effect effectInstance = m_EffectPrototype.Clone();
        effectInstance.ResetDuration();
        effectInstance.enabled = true;

        tankInfo.m_TankEffectManager.AddEffect(effectInstance);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var tankInfo = other.GetComponent<TankInfo>();

            if (tankInfo == null) return;

            m_TanksInArea.Remove(tankInfo);
        }
    }
}

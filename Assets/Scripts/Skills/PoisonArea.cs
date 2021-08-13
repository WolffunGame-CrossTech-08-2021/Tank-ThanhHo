using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    [SerializeField] float m_PoisonRadius;
    [SerializeField] float m_PoisonEffectDuration;
    [SerializeField] float m_AreaDuration;
    [SerializeField] float m_Dps;

    public PoisonEffect m_PoisonEffectPrefab;

    List<TankInfo> m_TanksInArea;

    TankInfo m_Owner;

    public void SetUp(TankInfo owner,float poisonRadius, float areaDuration, float poisonEffectDuration, float dps)
    {
        m_Owner = owner;

        m_PoisonRadius = poisonRadius;
        m_PoisonEffectDuration = poisonEffectDuration;
        m_AreaDuration = areaDuration;
        m_Dps = dps;

        transform.localScale = new Vector3(poisonRadius, poisonRadius, poisonRadius);
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
        PoisonEffect poisonEffectInstance = Instantiate(m_PoisonEffectPrefab);
        poisonEffectInstance.m_Dps = m_Dps;
        poisonEffectInstance.m_MaxDuration = m_PoisonEffectDuration;


        tankInfo.AddEffect(poisonEffectInstance);
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

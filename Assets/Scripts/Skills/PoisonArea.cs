using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    [SerializeField] float m_PoisonRadius;
    [SerializeField] float m_PoisonEffectDuration;
    [SerializeField] float m_AreaDuration;
    [SerializeField] float m_Dps;
    [SerializeField] PoisonEffect m_PoisonEffectPrefab;
    [SerializeField] SphereCollider poisonAreaCollider;

    Dictionary<TankInfo, PoisonEffect> m_AffectedTanks;
    Dictionary<TankInfo, PoisonEffect> m_TanksInArea;

    TankInfo m_Owner;

    public void SetUp(TankInfo owner,float poisonRadius, float areaDuration, float poisonEffectDuration, float dps)
    {
        m_Owner = owner;

        m_PoisonRadius = poisonRadius;
        m_PoisonEffectDuration = poisonEffectDuration;
        m_AreaDuration = areaDuration;
        m_Dps = dps;

        transform.localScale = new Vector3(poisonRadius, poisonRadius, poisonRadius);
        m_AffectedTanks = new Dictionary<TankInfo, PoisonEffect>();
        m_TanksInArea = new Dictionary<TankInfo, PoisonEffect>();
    }

    private void Start()
    {
        Destroy(gameObject, m_AreaDuration);
    }

    private void Update()
    {
        foreach(var tank in m_TanksInArea)
        {
            tank.Value.ResetDuration();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var tankInfo = other.GetComponent<TankInfo>();

            if (tankInfo == null) return;

            if (tankInfo == m_Owner) return;

            if(!m_AffectedTanks.ContainsKey(tankInfo) || m_AffectedTanks[tankInfo] == null)
            {
                PoisonEffect poisonEffectInstance = Instantiate(m_PoisonEffectPrefab);
                poisonEffectInstance.m_Dps = m_Dps;
                poisonEffectInstance.m_MaxDuration = m_PoisonEffectDuration;
                

                tankInfo.AddEffect(poisonEffectInstance);
                m_AffectedTanks[tankInfo] = poisonEffectInstance;
            }
            else
            {
                m_AffectedTanks[tankInfo].ResetDuration();
            }

            m_TanksInArea[tankInfo] = m_AffectedTanks[tankInfo];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var tankInfo = other.GetComponent<TankInfo>();

            if (tankInfo == null) return;

            m_TanksInArea.Remove(tankInfo);
        }
    }
}

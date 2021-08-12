using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoisonAreaTodo : BaseShellTodo
{
    [SerializeField] PoisonArea m_PoisonAreaPrefab;
    [SerializeField] ParticleSystem m_ExplossionParticlePrefab;
    [SerializeField] float m_PoisonRadius;
    [SerializeField] float m_PoisonAreaDuration;
    [SerializeField] float m_PoisonEffectDuration;
    [SerializeField] float m_Dps;

    private void CreatePoisonArea(Shell shell, float poisonRadius, float poisonAreaDuration, float poisonEffectDuration, float dps)
    {
        Vector3 shellPosition = shell.transform.position;

        ParticleSystem explossionParticle = Instantiate(m_ExplossionParticlePrefab);
        explossionParticle.transform.parent = null;
        explossionParticle.transform.position = shellPosition;
        //m_ExplossionParticle.transform.position = transform.position;
        explossionParticle.Play();

        Vector3 poisonAreaPosition = shellPosition;
        poisonAreaPosition.y = 0;
        PoisonArea poisonAreaInstance = Instantiate(m_PoisonAreaPrefab, poisonAreaPosition, Quaternion.identity);
        poisonAreaInstance.SetUp(shell.m_Owner, poisonRadius, poisonAreaDuration, poisonEffectDuration, dps);
    }

    public override void Execute(Shell shell)
    {
        CreatePoisonArea(shell, m_PoisonRadius, m_PoisonAreaDuration, m_PoisonEffectDuration, m_Dps);
    }
}

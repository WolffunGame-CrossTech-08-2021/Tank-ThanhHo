using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoisonAreaTodo : BaseShellTodo
{
    [SerializeField] EffectedArea m_PoisonAreaPrefab;
    [SerializeField] ParticleSystem m_ExplossionParticlePrefab;
    public float m_PoisonRadius;
    public float m_PoisonAreaDuration;
    public float m_PoisonEffectDuration;
    public float m_Dps;

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

        PoisonEffect poisonEffectInstance = EffectPoolFamily.m_Instance.GetObject(EffectEnum.Poison) as PoisonEffect;

        poisonEffectInstance.m_MaxDuration = m_PoisonEffectDuration;
        poisonEffectInstance.m_Dps = m_Dps;

        EffectedArea poisonAreaInstance = Instantiate(m_PoisonAreaPrefab, poisonAreaPosition, Quaternion.identity);
        poisonAreaInstance.SetUp(shell.m_Owner, poisonRadius, poisonAreaDuration, poisonEffectInstance);
    }

    public override void Execute(Shell shell)
    {
        CreatePoisonArea(shell, m_PoisonRadius, m_PoisonAreaDuration, m_PoisonEffectDuration, m_Dps);
    }

    public override ShellTodoEnum GetShellTodoType()
    {
        return ShellTodoEnum.PoisonArea;
    }

    public override BaseShellTodo Clone()
    {
        PoisonAreaTodo todoInstance = ShellTodoPoolFamily.m_Instance.GetObject(GetShellTodoType()) as PoisonAreaTodo;

        todoInstance.m_PoisonAreaPrefab = m_PoisonAreaPrefab;
        todoInstance.m_ExplossionParticlePrefab = m_ExplossionParticlePrefab;
        todoInstance.m_PoisonRadius = m_PoisonRadius;
        todoInstance.m_PoisonAreaDuration = m_PoisonAreaDuration;
        todoInstance.m_PoisonEffectDuration = m_PoisonEffectDuration;
        todoInstance.m_Dps = m_Dps;

        return todoInstance;
    }
}

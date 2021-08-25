using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAreaTodo : BaseShellTodo
{
    [SerializeField] EffectedArea m_EffectedAreaPrefab;
    [SerializeField] ParticleSystem m_ExplossionParticlePrefab;
    public float m_AreaRadius;
    public float m_AreaDuration;
    public float m_EffectDuration;
    public float m_SlowValue;

    private void CreateEffectedArea(Shell shell)
    {
        Vector3 shellPosition = shell.transform.position;

        ParticleSystem explossionParticle = Instantiate(m_ExplossionParticlePrefab);
        explossionParticle.transform.parent = null;
        explossionParticle.transform.position = shellPosition;
        //m_ExplossionParticle.transform.position = transform.position;
        explossionParticle.Play();

        Vector3 poisonAreaPosition = shellPosition;
        poisonAreaPosition.y = 0;

        StickyEffect stickyEffectInstance = EffectPoolFamily.m_Instance.GetObject(EffectEnum.Sticky) as StickyEffect;

        stickyEffectInstance.m_MaxDuration = m_EffectDuration;
        stickyEffectInstance.m_FlatSlowValue = m_SlowValue;
        stickyEffectInstance.m_Owner = shell.m_Owner;

        EffectedArea poisonAreaInstance = Instantiate(m_EffectedAreaPrefab, poisonAreaPosition, Quaternion.identity);
        poisonAreaInstance.SetUp(shell.m_Owner, m_AreaRadius, m_AreaDuration, stickyEffectInstance);
    }

    public override void Execute(Shell shell)
    {
        CreateEffectedArea(shell);
    }

    public override ShellTodoEnum GetShellTodoType()
    {
        return ShellTodoEnum.SlowArea;
    }

    public override BaseShellTodo Clone()
    {
        SlowAreaTodo todoInstance = ShellTodoPoolFamily.m_Instance.GetObject(GetShellTodoType()) as SlowAreaTodo;
        todoInstance.m_EffectedAreaPrefab = m_EffectedAreaPrefab;
        todoInstance.m_ExplossionParticlePrefab = m_ExplossionParticlePrefab;

        todoInstance.m_AreaRadius = m_AreaRadius;
        todoInstance.m_AreaDuration = m_AreaDuration;
        todoInstance.m_EffectDuration = m_EffectDuration;
        todoInstance.m_SlowValue = m_SlowValue;

        return todoInstance;
    }
}

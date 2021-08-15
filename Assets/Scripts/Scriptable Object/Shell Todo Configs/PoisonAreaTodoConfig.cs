using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poison Area Shell Todo", menuName = "Scriptable Object/Shell Todos/Poison Area Shell Todo")]
public class PoisonAreaTodoConfig : BaseShellTodoConfig
{
    public float m_PoisonRadius;
    public float m_PoisonAreaDuration;
    public float m_PoisonEffectDuration;
    public float m_Dps;

    public override BaseShellTodo GetShellTodo()
    {
        if(!(m_ShellTodoPrefab is PoisonAreaTodo))
        {
            Debug.LogError("Poison area todo config cannot create todo that is not type of PoisonAreaTodo");
            return null;
        }

        PoisonAreaTodo todoinstance = base.GetShellTodo() as PoisonAreaTodo;
        todoinstance.m_PoisonRadius = m_PoisonRadius;
        todoinstance.m_PoisonAreaDuration = m_PoisonAreaDuration;
        todoinstance.m_PoisonEffectDuration = m_PoisonEffectDuration;
        todoinstance.m_Dps = m_Dps;

        return todoinstance;
    }

    protected override Type GetDesiredTodoType()
    {
        return typeof(PoisonAreaTodo);
    }
}

﻿using System;
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
        PoisonAreaTodo todoInstance = base.GetShellTodo() as PoisonAreaTodo;
        todoInstance.m_PoisonRadius = m_PoisonRadius;
        todoInstance.m_PoisonAreaDuration = m_PoisonAreaDuration;
        todoInstance.m_PoisonEffectDuration = m_PoisonEffectDuration;
        todoInstance.m_Dps = m_Dps;

        return todoInstance;
    }

    protected override Type GetDesiredTodoType()
    {
        return typeof(PoisonAreaTodo);
    }
}

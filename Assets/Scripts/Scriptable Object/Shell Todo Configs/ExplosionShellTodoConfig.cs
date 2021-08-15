using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion Shell Todo", menuName = "Scriptable Object/Shell Todos/Explosion Shell Todo")]
public class ExplosionShellTodoConfig : BaseShellTodoConfig
{
    public float m_MaxDamage = 100f;
    public float m_ExplosionForce = 1000f;
    public float m_ExplosionRadius = 5f;

    public override BaseShellTodo GetShellTodo()
    {
        if(!(m_ShellTodoPrefab is ExplosionShellTodo))
        {
            Debug.LogError("Explosion shell todo config cannot create todo that is not type of ExplosionShellTodo");
            return null;
        }

        ExplosionShellTodo todoInstance = base.GetShellTodo() as ExplosionShellTodo;
        todoInstance.m_MaxDamage = m_MaxDamage;
        todoInstance.m_ExplosionForce = m_ExplosionForce;
        todoInstance.m_ExplosionRadius = m_ExplosionRadius;

        return todoInstance;
    }

    protected override Type GetDesiredTodoType()
    {
        return typeof(ExplosionShellTodo);
    }
}

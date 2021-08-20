using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion Stun Todo", menuName = "Scriptable Object/Shell Todos/Explosion Stun Todo")]
public class ExplodeStunTodoConfig : BaseShellTodoConfig
{
    public float m_StunDuration;
    public float m_ExplosionRadius;

    public override BaseShellTodo GetShellTodo()
    {
        ExplodeStunTodo todoInstance = base.GetShellTodo() as ExplodeStunTodo;
        todoInstance.m_StunDuration = m_StunDuration;
        todoInstance.m_ExplosionRadius = m_ExplosionRadius;

        return todoInstance;
    }
}

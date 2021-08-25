using System;
using UnityEngine;

public abstract class BaseShellTodo : MonoBehaviour
{
    public abstract void Execute(Shell shell);
    public virtual void Destroy()
    {
        ShellTodoPoolFamily.m_Instance.ReturnObjectToPool(GetShellTodoType(), this);
    }

    public abstract ShellTodoEnum GetShellTodoType();

    public abstract BaseShellTodo Clone();
}

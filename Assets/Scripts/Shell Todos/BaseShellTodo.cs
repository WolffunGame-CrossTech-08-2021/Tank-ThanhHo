using System;
using UnityEngine;

public abstract class BaseShellTodo : MonoBehaviour
{
    public abstract void Execute(Shell shell);
    public virtual void Destroy()
    {
        if(gameObject != null)
            Destroy(gameObject);
    }
}

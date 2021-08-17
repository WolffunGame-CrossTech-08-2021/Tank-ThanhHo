using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShellTodoConfig : ScriptableObject
{
    public ShellTodoEnum m_ShellTodoType;

    //private void OnValidate()
    //{
    //    if (m_ShellTodoPrefab == null) return;

    //    System.Type prefabType = m_ShellTodoPrefab.GetType();
    //    System.Type desiredType = GetDesiredTodoType();

    //    if (!(prefabType == desiredType || prefabType.IsSubclassOf(desiredType)))
    //    {
    //        Debug.LogError("Canon type is not supported");
    //        m_ShellTodoPrefab = null;
    //        return;
    //    }
    //}

    public virtual BaseShellTodo GetShellTodo()
    {
        BaseShellTodo shellTodoInstance = ShellTodoPoolFamily.m_Instance.GetObject(m_ShellTodoType);

        return shellTodoInstance;
    }

    protected virtual System.Type GetDesiredTodoType()
    {
        return typeof(BaseShellTodo);
    }
}

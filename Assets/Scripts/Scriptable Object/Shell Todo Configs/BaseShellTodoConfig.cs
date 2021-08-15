using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShellTodoConfig : ScriptableObject
{
    public BaseShellTodo m_ShellTodoPrefab;

    private void OnValidate()
    {
        if (m_ShellTodoPrefab == null) return;

        System.Type prefabType = m_ShellTodoPrefab.GetType();
        System.Type desiredType = GetDesiredTodoType();

        if (!(prefabType == desiredType || prefabType.IsSubclassOf(desiredType)))
        {
            Debug.LogError("Canon type is not supported");
            m_ShellTodoPrefab = null;
            return;
        }
    }

    public virtual BaseShellTodo GetShellTodo()
    {
        BaseShellTodo shellTodoInstance = Instantiate(m_ShellTodoPrefab);

        return shellTodoInstance;
    }

    protected virtual System.Type GetDesiredTodoType()
    {
        return typeof(BaseShellTodo);
    }
}

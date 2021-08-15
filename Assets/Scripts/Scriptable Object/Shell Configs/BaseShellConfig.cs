using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base Shell Config", menuName = "Scriptable Object/Shells/Base Shell Config")]
public class BaseShellConfig : ScriptableObject
{
    public Shell m_ShellPrefab;
    public float m_MaxTimeToLive;

    public List<BaseShellTodoConfig> m_TimeOutTodoConfigs;
    public List<BaseShellTodoConfig> m_ExplodeTodoConfigs;

    private void OnValidate()
    {
        if (m_ShellPrefab == null) return;

        System.Type prefabType = m_ShellPrefab.GetType();
        System.Type desiredType = GetDesiredShellType();

        if (!(prefabType == desiredType || prefabType.IsSubclassOf(desiredType)))
        {
            Debug.LogError("Canon type is not supported");
            m_ShellPrefab = null;
            return;
        }
    }

    public virtual Shell GetShell()
    {
        Shell shellInstance = Instantiate(m_ShellPrefab);
        shellInstance.m_MaxTimeToLive = m_MaxTimeToLive;

        shellInstance.ClearTimeOutTodos();
        for (int i = 0; i < m_TimeOutTodoConfigs.Count; i++)
        {
            shellInstance.AddTimeOutTodo(m_TimeOutTodoConfigs[i].GetShellTodo());
        }

        shellInstance.ClearExplodeTodo();
        for (int i = 0; i < m_ExplodeTodoConfigs.Count; i++)
        {
            shellInstance.AddExplodeTodo(m_ExplodeTodoConfigs[i].GetShellTodo());
        }

        return shellInstance;
    }

    protected virtual System.Type GetDesiredShellType()
    {
        return typeof(Shell);
    }
}

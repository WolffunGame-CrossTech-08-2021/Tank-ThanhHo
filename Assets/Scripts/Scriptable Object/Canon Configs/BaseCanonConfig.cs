using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCanonConfig : ScriptableObject
{
    public BaseCanon m_CanonPrefab;
    //public BaseShellConfig m_ShellConfig;
    public Shell m_ShellPrefab;

    private void OnValidate()
    {
        if (m_CanonPrefab == null) return;

        System.Type prefabType = m_CanonPrefab.GetType();
        System.Type desiredType = GetDesiredCanonType();

        if (!(prefabType == desiredType || prefabType.IsSubclassOf(desiredType)))
        {
            Debug.LogError("Canon type is not supported");
            m_CanonPrefab = null;
            return;
        }
    }

    public virtual BaseCanon GetCanon()
    {
        BaseCanon canonInstance = Instantiate(m_CanonPrefab);
        canonInstance.m_ShellPrefab = m_ShellPrefab;

        return canonInstance;
    }

    protected virtual System.Type GetDesiredCanonType()
    {
        return typeof(BaseCanon);
    }
}

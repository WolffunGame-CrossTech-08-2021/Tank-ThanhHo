﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public abstract class BaseCanon : MonoBehaviour
{
    [SerializeField] protected AudioSource m_ShootingAudio;
    [SerializeField] protected AudioClip m_FireClip;
    [SerializeField] List<BaseShellTodo> m_ExplodeTodoPrefabs;
    [SerializeField] List<BaseShellTodo> m_TimeOutTodoPrefabs;
    [HideInInspector] public Transform m_FireTransform;
    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public TankInfo m_Owner;
    public Shell m_ShellPrefab;

    public System.Action<Shell> OnCreateShell;
    protected int m_Layer;

    virtual protected void Start()
    {
        StringBuilder layerName = new StringBuilder("Shell Hitbox P");
        layerName.Append(m_PlayerNumber.ToString());
        Debug.Log(layerName.ToString());
        m_Layer = LayerMask.NameToLayer(layerName.ToString());
    }

    public abstract void Activate();

    public abstract void Deactivate();

    public void SetFireTransform(Transform fireTransform)
    {
        m_FireTransform = fireTransform;
    }

    public void SetPlayerNumber(int playerNumber)
    {
        m_PlayerNumber = playerNumber;
    }

    public void SetOwner(TankInfo owner)
    {
        m_Owner = owner;
    }

    protected void PlayShootingAudio()
    {
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.loop = false;
        m_ShootingAudio.Play();
    }

    protected virtual Shell InstantiateShell()
    {
        Shell shellInstance = Instantiate(m_ShellPrefab);

        shellInstance.m_Owner = m_Owner;
        shellInstance.gameObject.SetActive(true);
        shellInstance.SetLayer(m_Layer);

        for (int i = 0; i < m_TimeOutTodoPrefabs.Count; i++)
        {
            BaseShellTodo shellTodoInstance = Instantiate(m_TimeOutTodoPrefabs[i]);
            shellInstance.AddTimeOutTodo(shellTodoInstance);
        }

        for (int i = 0; i < m_ExplodeTodoPrefabs.Count; i++)
        {
            BaseShellTodo shellTodoInstance = Instantiate(m_ExplodeTodoPrefabs[i]);
            shellInstance.AddExplodeTodo(shellTodoInstance);
        }

        return shellInstance;
    }

    protected virtual void Fire()
    {
        PlayShootingAudio();
    }

    protected virtual void NotifyCreateShell(Shell shell)
    {
        if(OnCreateShell != null)
        {
            OnCreateShell(shell);
        }
    }
}

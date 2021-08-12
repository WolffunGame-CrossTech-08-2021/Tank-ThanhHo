﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shell : MonoBehaviour
{
    public TankInfo m_Owner;
    public float m_MaxTimeToLive;
    public Rigidbody m_RigidBody;
    protected List<BaseShellTodo> m_TimeOutTodos;
    protected List<BaseShellTodo> m_ExplodeTodos;

    protected float m_CurrentTimeToLive;

    protected virtual void Start()
    {
        m_CurrentTimeToLive = m_MaxTimeToLive;
    }

    protected virtual void Update()
    {
        transform.forward = m_RigidBody.velocity.normalized;

        m_CurrentTimeToLive -= Time.deltaTime;

        Debug.Log(m_CurrentTimeToLive);

        if(m_CurrentTimeToLive <= 0)
        {
            OnEndTimeToLive();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        OnExplode();
    }

    protected virtual void OnEndTimeToLive()
    {
        if(m_TimeOutTodos != null)
        {
            for (int i = 0; i < m_TimeOutTodos.Count; i++)
            {
                m_TimeOutTodos[i].Execute(this);
            }
        }

        Destroy(this.gameObject);
    }

    protected virtual void OnExplode()
    {
        if(m_ExplodeTodos != null)
        {
            for (int i = 0; i < m_ExplodeTodos.Count; i++)
            {
                m_ExplodeTodos[i].Execute(this);
            }
        }

        Destroy(this.gameObject);
    }

    protected virtual void OnDestroy()
    {
        ClearTimeOutTodos();
        ClearExplodeTodo();
    }

    public virtual void AddTimeOutTodo(BaseShellTodo todo)
    {
        if (m_TimeOutTodos == null)
            m_TimeOutTodos = new List<BaseShellTodo>();
        m_TimeOutTodos.Add(todo);
    }

    public virtual void AddExplodeTodo(BaseShellTodo todo)
    {
        if (m_ExplodeTodos == null)
            m_ExplodeTodos = new List<BaseShellTodo>();
        m_ExplodeTodos.Add(todo);
    }

    public void ClearTimeOutTodos()
    {
        if (m_TimeOutTodos == null) return;

        for(int i=0; i<m_TimeOutTodos.Count; i++)
        {
            m_TimeOutTodos[i].Destroy();
        }
            
        m_TimeOutTodos.Clear();
    }

    public void ClearExplodeTodo()
    {
        if (m_ExplodeTodos == null) return;

        for (int i = 0; i < m_ExplodeTodos.Count; i++)
        {
            m_ExplodeTodos[i].Destroy();
        }

        m_ExplodeTodos.Clear();
    }
}

using UnityEngine;
using System.Collections.Generic;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    [SerializeField] List<BaseCanonConfig> m_BaseCanonConfigs;
    public Transform m_FireTransform;
    public TankInfo m_TankInfo;

    private string m_SwitchWeaponButton;

    private BaseCanon m_CurrentCanon;
    private int m_CurrentCanonIndex;

    public List<BaseShellTodo> m_TimeOutTodos;
    public List<BaseShellTodo> m_ExplodeTodos;

    private void OnEnable()
    {

    }


    private void Start()
    {
        m_CurrentCanonIndex = 0;
        ChooseCanon(m_CurrentCanonIndex);
        m_SwitchWeaponButton = "Player" + m_PlayerNumber + "_SwitchWeapon";
    }

    private void ChooseCanon(int index)
    {
        if (index >= m_BaseCanonConfigs.Count) return;

        if (m_CurrentCanon != null)
        {
            Destroy(m_CurrentCanon);
        }

        m_CurrentCanon = m_BaseCanonConfigs[index].GetCanon();

        Transform canonTransform = m_CurrentCanon.transform;
        Transform tankShootingTransform = transform;

        canonTransform.parent = tankShootingTransform;
        canonTransform.localPosition = Vector3.zero;
        canonTransform.localRotation = Quaternion.identity;

        m_CurrentCanon.m_Owner = m_TankInfo;
        m_CurrentCanon.m_FireTransform = m_FireTransform;
        m_CurrentCanon.m_PlayerNumber = m_PlayerNumber;

        m_CurrentCanon.OnCreateShell = OnCanonCreateShell;

        m_CurrentCanon.Activate();
    }

    private void Update()
    {
        if (Input.GetButtonDown(m_SwitchWeaponButton))
        {
            m_CurrentCanonIndex++;
            m_CurrentCanonIndex %= m_BaseCanonConfigs.Count;

            ChooseCanon(m_CurrentCanonIndex);
        }
    }

    public void Activate()
    {
        if(m_CurrentCanon != null)
        {
            m_CurrentCanon.Activate();
        }
    }

    public void Deactivate()
    {
        if (m_CurrentCanon != null)
        {
            m_CurrentCanon.Deactivate();
        }
    }

    private void OnCanonCreateShell(Shell shell)
    {
        foreach(var todo in m_TimeOutTodos)
        {
            shell.AddTimeOutTodo(todo.Clone());
        }

        foreach(var todo in m_ExplodeTodos)
        {
            shell.AddExplodeTodo(todo.Clone());
        }
    }
}
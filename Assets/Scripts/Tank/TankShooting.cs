using UnityEngine;
using System.Collections.Generic;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    [SerializeField] BaseCanon m_CanonPrefab;
    [SerializeField] Shell m_ShellPrefab;
    [SerializeField] List<BaseShellTodo> m_ExplodeTodoPrefabs;
    [SerializeField] List<BaseShellTodo> m_TimeOutTodoPrefabs;
    [SerializeField] Transform m_FireTransform;
    [SerializeField] TankInfo m_TankInfo;

    private string m_Bullet1Button;
    private string m_Bullet2Button;
    private string m_Bullet3Button;
    private string m_Bullet4Button;

    private BaseCanon m_CurrentCanon;

    private void OnEnable()
    {
    }


    private void Start()
    {
        m_CurrentCanon = Instantiate(m_CanonPrefab);
        m_CurrentCanon.transform.parent = transform;

        m_CurrentCanon.SetOwner(m_TankInfo);
        m_CurrentCanon.m_Shell = m_ShellPrefab;
        m_CurrentCanon.m_FireTransform = m_FireTransform;
        m_CurrentCanon.m_PlayerNumber = m_PlayerNumber;
        m_CurrentCanon.Activate();
        m_CurrentCanon.OnCreateShell = OnCreateShell;

        //if (m_Shells.Count <= 0) return;

        //m_Bullet1Button = "Player" + m_PlayerNumber + "_Bullet1";
        //m_Bullet2Button = "Player" + m_PlayerNumber + "_Bullet2";
        //m_Bullet3Button = "Player" + m_PlayerNumber + "_Bullet3";
        //m_Bullet4Button = "Player" + m_PlayerNumber + "_Bullet4";

        //ChooseBullet(0);
    }

    private void OnCreateShell(Shell shellInstance)
    {
        for(int i=0; i<m_TimeOutTodoPrefabs.Count; i++)
        {
            BaseShellTodo shellTodoInstance = Instantiate(m_TimeOutTodoPrefabs[i]);
            shellInstance.AddTimeOutTodo(shellTodoInstance);
        }

        for (int i = 0; i < m_ExplodeTodoPrefabs.Count; i++)
        {
            BaseShellTodo shellTodoInstance = Instantiate(m_ExplodeTodoPrefabs[i]);
            shellInstance.AddExplodeTodo(shellTodoInstance);
        }
    }

    private void ChooseBullet(int index)
    {
        //if(m_CurrentActivator != null)
        //{
        //    Destroy(m_CurrentActivator.gameObject);
        //}

        //m_CurrentActivator = m_Shells[index].GetActivator();

        //m_CurrentActivator.SetFireTransform(m_FireTransform);
        //m_CurrentActivator.SetOwner(m_TankInfo);
        //m_CurrentActivator.SetPlayerNumber(m_PlayerNumber);

        //m_CurrentActivator.transform.parent = transform;
        //m_CurrentActivator.transform.position = transform.position;
        //m_CurrentActivator.transform.localRotation = Quaternion.Euler(Vector3.zero);

        //m_CurrentActivator.Activate();
    }

    private void Update()
    {
        //if(Input.GetButtonDown(m_Bullet1Button))
        //{
        //    ChooseBullet(0);
        //}
        //else if (Input.GetButtonDown(m_Bullet2Button))
        //{
        //    Debug.Log("cnsa");
        //    ChooseBullet(1);
        //}
        //else if (Input.GetButtonDown(m_Bullet3Button))
        //{
        //    ChooseBullet(2);
        //}
        //else if (Input.GetButtonDown(m_Bullet4Button))
        //{
        //    ChooseBullet(3);
        //}

    }
}
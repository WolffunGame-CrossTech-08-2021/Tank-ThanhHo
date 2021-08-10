using UnityEngine;
using System.Collections.Generic;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    [SerializeField] List<Shell> m_Shells;
    [SerializeField] Transform m_FireTransform;
    [SerializeField] TankInfo m_TankInfo;

    private BaseShellActivator m_CurrentActivator;
    private string m_Bullet1Button;
    private string m_Bullet2Button;
    private string m_Bullet3Button;
    private string m_Bullet4Button;

    private void OnEnable()
    {
    }


    private void Start()
    {
        if (m_Shells.Count <= 0) return;

        m_Bullet1Button = "Player" + m_PlayerNumber + "_Bullet1";
        m_Bullet2Button = "Player" + m_PlayerNumber + "_Bullet2";
        m_Bullet3Button = "Player" + m_PlayerNumber + "_Bullet3";
        m_Bullet4Button = "Player" + m_PlayerNumber + "_Bullet4";

        ChooseBullet(0);
    }

    private void ChooseBullet(int index)
    {
        if(m_CurrentActivator != null)
        {
            Destroy(m_CurrentActivator.gameObject);
        }

        m_CurrentActivator = m_Shells[index].GetActivator();

        m_CurrentActivator.SetFireTransform(m_FireTransform);
        m_CurrentActivator.SetOwner(m_TankInfo);
        m_CurrentActivator.SetPlayerNumber(m_PlayerNumber);

        m_CurrentActivator.transform.parent = transform;
        m_CurrentActivator.transform.position = transform.position;
        m_CurrentActivator.transform.localRotation = Quaternion.Euler(Vector3.zero);

        m_CurrentActivator.Activate();
    }

    private void Update()
    {
        if(Input.GetButtonDown(m_Bullet1Button))
        {
            ChooseBullet(0);
        }
        else if (Input.GetButtonDown(m_Bullet2Button))
        {
            Debug.Log("cnsa");
            ChooseBullet(1);
        }
        else if (Input.GetButtonDown(m_Bullet3Button))
        {
            ChooseBullet(2);
        }
        else if (Input.GetButtonDown(m_Bullet4Button))
        {
            ChooseBullet(3);
        }

    }
}
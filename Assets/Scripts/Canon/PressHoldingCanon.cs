using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressHoldingCanon : BaseCanon
{
    [SerializeField] Slider m_AimSlider;
    [SerializeField] AudioClip m_ChargingClip;
    [SerializeField] float m_MinLaunchForce = 15f;
    [SerializeField] float m_MaxLaunchForce = 30f;
    [SerializeField] float m_MaxChargeTime = 0.75f;

    private enum GunState
    {
        Idle,
        Charging,
        Fire,
    }

    GunState m_Gunstate;
    private string m_FireButton;
    protected float m_CurrentLaunchForce;
    private float m_ChargeSpeed;
    private bool m_Activated = false;

    protected override void Start()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        m_Gunstate = GunState.Idle;

        base.Start();
    }

    public override void Activate()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

        m_Activated = true;
    }

    public override void Deactivate()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        m_Gunstate = GunState.Idle;

        m_Activated = false;
    }

    public void SetShell(Shell shell)
    {
        m_Shell = shell;
    }

    void Update()
    {
        if (!m_Activated) return;

        if (m_Gunstate == GunState.Idle)
        {
            m_AimSlider.value = m_MinLaunchForce;
            if (Input.GetButtonDown(m_FireButton))
            {
                m_Gunstate = GunState.Charging;
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.loop = true;
                m_ShootingAudio.Play();
            }
        }
        else if (m_Gunstate == GunState.Charging)
        {
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
            m_AimSlider.value = m_CurrentLaunchForce;


            if (Input.GetButtonUp(m_FireButton))
            {
                m_Gunstate = GunState.Fire;
            }

            if (m_CurrentLaunchForce >= m_MaxLaunchForce)
            {
                m_CurrentLaunchForce = m_MaxLaunchForce;
                m_Gunstate = GunState.Fire;

            }
        }
        else if (m_Gunstate == GunState.Fire)
        {
            Fire();

            m_CurrentLaunchForce = m_MinLaunchForce;
            m_Gunstate = GunState.Idle;
            m_AimSlider.value = m_MinLaunchForce;
        }
        else
        {
            Debug.LogError("Tank shooting goes into undefined state");
        }
    }

    protected override void Fire()
    {
        Shell shell = InstantiateShell();

        Rigidbody shellRigidBody = shell.m_RigidBody;
        shellRigidBody.transform.position = m_FireTransform.position;
        shellRigidBody.transform.rotation = m_FireTransform.rotation;

        shellRigidBody.velocity = m_CurrentLaunchForce * shell.transform.forward;

        NotifyCreateShell(shell);

        base.Fire();
    }
}

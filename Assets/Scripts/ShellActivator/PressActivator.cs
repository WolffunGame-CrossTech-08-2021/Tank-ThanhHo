using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressActivator : BaseShellActivator
{
    [SerializeField] AudioSource m_ShootingAudio;
    [SerializeField] AudioClip m_FireClip;

    private bool activated = false;
    private string m_FireButton;
    private Shell m_Shell;

    public override void Activate()
    {
        activated = true;
    }

    public override void Deactivate()
    {
        activated = false;
    }

    public void SetShell(Shell shell)
    {
        m_Shell = shell;
    }

    void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated == false) return;

        if(Input.GetButtonDown(m_FireButton))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (!(m_Shell is DirectionalShell))
        {
            Debug.Log("Press Activator: Shell is not directional shell");
            return;
        }

        Shell shellInstance = Instantiate(m_Shell);

        shellInstance.m_Owner = m_Owner;
        DirectionalShell shellDirection = shellInstance as DirectionalShell;

        shellInstance.transform.position = m_FireTransform.position;
        shellDirection.SetDirection(m_FireTransform.forward);

        PlayShootingAudio();
    }

    private void PlayShootingAudio()
    {
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.loop = false;
        m_ShootingAudio.Play();
    }
}

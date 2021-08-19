using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressCanon : BaseCanon
{
    private bool activated = false;

    public override void Activate()
    {
        activated = true;
    }

    public override void Deactivate()
    {
        activated = false;
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

    protected override void Fire()
    {
        //if (!(m_ShellConfig.m_ShellPrefab is IDirectionalShell))
        //{
        //    Debug.Log("Press Activator: Shell is not directional shell");
        //    return;
        //}

        Shell shellInstance = InstantiateShell();

        //IDirectionalShell shellDirection = shellInstance as IDirectionalShell;

        shellInstance.SetUp(m_FireTransform.position, m_FireTransform.forward, 0);
        //shellDirection.SetDirection(m_FireTransform.forward);

        NotifyCreateShell(shellInstance);

        base.Fire();
    }
}

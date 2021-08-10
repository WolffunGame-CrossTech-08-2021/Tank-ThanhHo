using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shell : MonoBehaviour
{
    public TankInfo m_Owner;

    public abstract BaseShellActivator GetActivator();
}

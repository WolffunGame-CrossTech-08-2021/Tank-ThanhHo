using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class TankInfo : MonoBehaviour
{
    public float m_Health;
    public float m_MovementSpeed;
    public float m_TurnSpeed;
    public float m_Damage;

    List<Effect> m_effects;

    private void OnEnable()
    {
        m_effects = new List<Effect>();
    }

    public ReadOnlyCollection<Effect> effects
    {
        get
        {
            return m_effects.AsReadOnly();
        }
    }

    /// <returns>
    /// True: Add success
    /// False: Add fail
    /// </returns>
    public bool AddEffect(Effect effect)
    {
        if(!m_effects.Contains(effect))
        {
            m_effects.Add(effect);
            effect.m_Target = this;
            return true;
        }

        return false;
    }

    public bool RemoveEffect(Effect effect)
    {
        return m_effects.Remove(effect);
    }
}

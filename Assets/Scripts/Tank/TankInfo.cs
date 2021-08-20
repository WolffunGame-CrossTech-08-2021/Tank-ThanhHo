using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class TankInfo : MonoBehaviour
{
    public float m_MovementSpeed;
    public float m_TurnSpeed;
    public float m_Damage;
    public TankHealth m_TankHealth;

    [SerializeField] List<Effect> m_effects;

    private void OnEnable()
    {
        if(m_effects == null)
        {
            m_effects = new List<Effect>();
        }
        else
        {
            m_effects.Clear();
        }
        
    }

    public ReadOnlyCollection<Effect> effects
    {
        get
        {
            return m_effects.AsReadOnly();
        }
    }

    public void AddEffect(Effect effect)
    {
        EffectEnum effectType = effect.GetEffectType();

        if(effect is IPassThroughable)
        {
            ApplyEffect(effect);
            return;
        }

        Effect existingEffect = GetEffect(effectType);

        if (existingEffect == null)
        {
            ApplyEffect(effect);
            return;
        }
        else
        {
            existingEffect.ResetDuration();

            if (effect is IStackable)
            {
                IStackable stackInfo = existingEffect as IStackable;

                stackInfo.IncreaseStack(1);
            }

            effect.Destroy();
            return;
        }

        
    }

    private void ApplyEffect(Effect effect)
    {
        m_effects.Add(effect);
        effect.m_Target = this;
        effect.StartEffect();
    }

    public bool RemoveEffect(Effect effect)
    {
        return m_effects.Remove(effect);
    }

    public Effect GetEffect(EffectEnum effectType)
    {
        return m_effects.Find(x => x.GetEffectType() == effectType);
    }
}

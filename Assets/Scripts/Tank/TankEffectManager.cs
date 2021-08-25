using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEffectManager : MonoBehaviour
{
    [SerializeField] List<Effect> m_Effects;
    [SerializeField] TankInfo m_TankInfo;

    private void OnEnable()
    {
        if (m_Effects == null)
        {
            m_Effects = new List<Effect>();
        }
        else
        {
            m_Effects.Clear();
        }
    }

    public void AddEffect(Effect effect)
    {
        EffectEnum effectType = effect.GetEffectType();

        if (effect is IPassThroughable)
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
        if(effect is IBlockMovement)
        {
            BlockMovement();
        }

        if(effect is IBlockShooting)
        {
            BlockShooting();
        }

        if(effect is IMovementModifier)
        {
            m_TankInfo.m_TankMovement.AddMovementModifier(effect as IMovementModifier);
        }

        m_Effects.Add(effect);
        effect.m_Target = m_TankInfo;
        effect.StartEffect();
    }

    public bool RemoveEffect(Effect effect)
    {
        bool result = m_Effects.Remove(effect);

        if (result == false) return false;

        if (effect is IBlockMovement)
        {
            if(!ExistBlockMovementEffect())
            {
                UnblockMovement();
            }
        }

        if (effect is IBlockShooting)
        {
            if (!ExistBlockShootingEffect())
            {
                UnblockShooting();
            }
        }

        if (effect is IMovementModifier)
        {
            m_TankInfo.m_TankMovement.RemoveMovementModifier(effect as IMovementModifier);
        }

        return true;
    }

    private bool ExistBlockMovementEffect()
    {
        for(int i=0; i<m_Effects.Count; i++)
        {
            if(m_Effects[i] is IBlockMovement)
            {
                return true;
            }
        }

        return false;
    }

    private bool ExistBlockShootingEffect()
    {
        for (int i = 0; i < m_Effects.Count; i++)
        {
            if (m_Effects[i] is IBlockShooting)
            {
                return true;
            }
        }

        return false;
    }

    public Effect GetEffect(EffectEnum effectType)
    {
        return m_Effects.Find(x => x.GetEffectType() == effectType);
    }

    private void BlockMovement()
    {
        m_TankInfo.m_TankMovement.Deactivate();
    }

    private void UnblockMovement()
    {
        m_TankInfo.m_TankMovement.Activate();
    }

    private void BlockShooting()
    {
        m_TankInfo.m_TankShooting.Deactivate();
    }

    private void UnblockShooting()
    {
        m_TankInfo.m_TankShooting.Activate();
    }
}

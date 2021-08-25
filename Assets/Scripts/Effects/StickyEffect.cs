using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyEffect : Effect, IMovementModifier
{
    public float m_FlatSlowValue;
    public float m_PercentSlowValue;

    public override EffectEnum GetEffectType()
    {
        return EffectEnum.Sticky;
    }

    public MovementModify ModifyMovement(MovementModify movementModify)
    {
        MovementModify newMovementModify = movementModify;
        newMovementModify.m_FlatMovementChange -= m_FlatSlowValue;
        newMovementModify.m_PercentMovementChange -= m_PercentSlowValue;

        return newMovementModify;
    }
}

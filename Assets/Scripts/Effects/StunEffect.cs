using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : Effect, IBlockMovement, IBlockShooting
{
    public override EffectEnum GetEffectType()
    {
        return EffectEnum.Stun;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemConfig : ScriptableObject
{
    [SerializeField] Sprite m_ItemSprite;

    public abstract BaseItem GetItem();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shell Buff Item Config", menuName = "Scriptable Object/Items/Shell Buff Item Config")]
public class ShellBuffItemConfig : BaseItemConfig
{
    [SerializeField] BaseShellTodo m_ShellTodoBuffPrefab;
    [SerializeField] float m_ItemDuration;
    [SerializeField] float m_BuffDuration;
    [SerializeField] EffectItem m_EffectItemPrefab;

    public override BaseItem GetItem()
    {
        ShellBuffEffect effectPrototype = EffectPoolFamily.m_Instance.GetObject(EffectEnum.BulletBuff) as ShellBuffEffect;
        effectPrototype.m_MaxDuration = m_BuffDuration;
        effectPrototype.enabled = false;
        effectPrototype.m_ShellTodoPrefab = m_ShellTodoBuffPrefab;

        EffectItem effectItem = Instantiate(m_EffectItemPrefab);
        effectItem.m_MaxDuration = m_ItemDuration;
        effectItem.m_EffectPrototype = effectPrototype;
        effectItem.SetItemSprite(m_ItemSprite);

        effectPrototype.transform.parent = effectItem.transform;

        return effectItem;
    }
}

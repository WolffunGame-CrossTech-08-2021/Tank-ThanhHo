using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem : MonoBehaviour
{
    private Image m_ItemImage;

    private void OnTriggerEnter(Collider other)
    {
        TankInfo tankInfo = other.GetComponent<TankInfo>();

        if(tankInfo != null)
        {
            OnTankCollect(tankInfo);
        }
    }

    protected abstract void OnTankCollect(TankInfo tankInfo);

    public void SetItemSprite(Sprite sprite)
    {
        m_ItemImage.sprite = sprite;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}

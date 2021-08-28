using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem : MonoBehaviour
{
    [SerializeField] private Image m_ItemImage;

    private void OnTriggerEnter(Collider other)
    {
        TankInfo tankInfo = other.GetComponent<TankInfo>();

        if(tankInfo != null)
        {
            OnTankCollect(tankInfo);
            Destroy();
        }
    }

    protected abstract void OnTankCollect(TankInfo tankInfo);

    public void SetItemSprite(Sprite sprite)
    {
        m_ItemImage.sprite = sprite;
    }

    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }
}

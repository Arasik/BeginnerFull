using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPGUI : MonoBehaviour
{
    [SerializeField] private Image mHPIcon;
    [SerializeField] private Image mHPIconRed;
    public Image[] mHPImages;
    private void Start() {
        mHPImages=GetComponentsInChildren<Image>();
        PlayerLogic.OnDamageEvent+=UpdateUI;
    }

    public void UpdateUI(float mHP,float mFullHP)
    {
        for(int i=0;i<mFullHP-mHP;i++)
        {
            mHPImages[i].sprite=mHPIconRed.sprite;
        }
    }
}

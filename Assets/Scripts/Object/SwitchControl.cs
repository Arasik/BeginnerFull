using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour
{

    
    private Animator m_Animator;
    private Animator m_PlayerAnimator;
    private AudioSource m_SwitchSource;
    public AudioClip m_SwitchClip;
    public bool m_LightStatus=true;
    public LightStatus m_LightControl;
    private void Start()
    {
        m_Animator=GetComponent<Animator>();    
        m_SwitchSource=GetComponent<AudioSource>();
    }
    
    public void SwitchLight(object o)
    {
        m_PlayerAnimator=o as Animator;
        
        if(!m_LightControl.m_NextStatus==true)
        {
            m_PlayerAnimator.SetBool(PlayerAniPara.SWITCH_ON,true);
            StartCoroutine(ReLightOn());
        }
        else if(!m_LightControl.m_NextStatus==false)
        {
            m_PlayerAnimator.SetBool(PlayerAniPara.SWITCH_OFF,true);
            StartCoroutine(ReLightOff());
        }
        m_SwitchSource.PlayOneShot(m_SwitchClip);
        m_Animator.SetBool(SwitchAniPara.SWTICH_STATUS,!m_LightControl.m_NextStatus);
        m_LightControl.SwitchStatu();   
    }
    IEnumerator ReLightOn()
    {
        yield return new WaitForSeconds(ConstSetting.DELAY_CALL_TIME_01);
        m_PlayerAnimator.SetBool(PlayerAniPara.SWITCH_ON,false);
    }
    IEnumerator ReLightOff()
    {
        yield return new WaitForSeconds(ConstSetting.DELAY_CALL_TIME_01);
        m_PlayerAnimator.SetBool(PlayerAniPara.SWITCH_OFF,false);
    }

}

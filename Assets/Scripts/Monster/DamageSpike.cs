using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpike : MonoBehaviour
{
    private Animator m_animator;
    private AudioSource m_DamageSource;
    public AudioClip m_DamageClip;
    private float DamageTime=0.0f;
    object[] m_damage =new object[2];//给予的伤害
    void Start()
    {
        m_damage[1]=tag;
        m_animator=GetComponent<Animator>();
        m_DamageSource=GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag==ObjectTag.PLAYER)
        {
            // 播放动画
            m_animator.SetBool(SpikeAniPara.IS_TRIGGER,true);
            m_DamageSource.PlayOneShot(m_DamageClip);
            //造成伤害
            m_damage[0]=ConstSetting.DAMAGE_UNIT;
            other.gameObject.GetComponent<PlayerLogic>().Damage(m_damage);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag==ObjectTag.PLAYER)
        {
            DamageTime+=Time.deltaTime;
            if(DamageTime>=ConstSetting.Damage_SPEED)
            {
                DamageTime-=ConstSetting.Damage_SPEED;
                other.gameObject.GetComponent<PlayerLogic>().Damage(m_damage);
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag==ObjectTag.PLAYER)
        {
            // 播放动画
            m_animator.SetBool(SpikeAniPara.IS_TRIGGER,false);
            m_damage[0]=ConstSetting.DAMAGE_NULL;
            other.gameObject.GetComponent<PlayerLogic>().Damage(m_damage);
        }
    }
}

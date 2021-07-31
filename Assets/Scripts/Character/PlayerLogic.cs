using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBaseUnit
{
    private bool m_IsDead;
    private float m_NowWaitDie=0.0f; 
    private Animator m_Animator;
    public GameEnding m_gameEnding;
    public HPGUI m_HPGUI;
    public float m_WaitDie=ConstSetting.WAIT_DIE_TIME;
    
    private void Start() 
    {
        m_HP=m_FullHP;
        m_Animator = GetComponent<Animator> ();
    }
    private void Update() 
    {
        if(m_IsDead)
        {
            m_NowWaitDie+=Time.deltaTime;
            if(m_NowWaitDie>=m_WaitDie)
            {
                m_gameEnding.CaughtPlayer();
            }
        }
    }
// 碰撞发生时
    

    private void Die()
    {
        //禁用移动
        GetComponent<PlayerMovement>().enabled=false;
        m_IsDead=true;
        m_Animator.SetBool(PlayerAniPara.IS_DEAD,m_IsDead);
    }

    public override void Damage(float m_damage)
    {
        if(m_IsDead==false)
        {
            m_HP-=m_damage;
            // m_HPGUI.UpdateUI(m_HP,m_FullHP);
            MultiCastEvents();
            if(m_HP<=0)
            {
                Die();
            }
        }
    }
    public override void Damage(object[] o)
    {
        
        float m_damage=(float)o[0];
        string m_damageType=(string)o[1];

        if(m_IsDead==false)
        {
            m_HP-=m_damage;
            m_HP=m_HP<ConstSetting.MIN_HP?ConstSetting.MIN_HP:m_HP;
            // m_HPGUI.UpdateUI(m_HP,m_FullHP);
            MultiCastEvents();
            if(m_HP<=0)
            {
                Die();
            }
            else if(m_damage!=ConstSetting.MIN_HP)
            {
                if(m_damageType==ConstSetting.DAMAGE_TYPE_MONSTER)
                {
                    m_Animator.SetBool(PlayerAniPara.MONSTER_DAMAGE,true);
                }
                else if(m_damageType==ConstSetting.DAMAGE_TYPE_TRAP)
                {
                    m_Animator.SetBool(PlayerAniPara.TRAP_DAMAGE,true);
                }
            }
            else if(m_damage==ConstSetting.MIN_HP)
            {
                if(m_damageType==ConstSetting.DAMAGE_TYPE_MONSTER)
                {
                    m_Animator.SetBool(PlayerAniPara.MONSTER_DAMAGE,false);
                }
                else if(m_damageType==ConstSetting.DAMAGE_TYPE_TRAP)
                {
                    m_Animator.SetBool(PlayerAniPara.TRAP_DAMAGE,false);
                }
            }
           
           
        }
        
    }
}

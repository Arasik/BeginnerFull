using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStatus : MonoBehaviour
{
    private float m_nowTime=ConstSetting.INIT_TIME;
    private float m_nowTimeReactive=ConstSetting.INIT_TIME;
    private float m_ReactiveTime=ConstSetting.REACTIVE_TIME;
    public bool m_NextStatus=true;

    private Light m_LightControl;
    public EnemiesManager m_EnemyMana;
    void Start()
    {
        m_LightControl=GetComponentsInChildren<Light>()[0];
    }


    void Update()
    {
        m_nowTime+=Time.deltaTime;
        if(m_NextStatus==false)//现在是关灯
        {
            m_nowTimeReactive+=Time.deltaTime;
            if(m_nowTimeReactive>=m_ReactiveTime)
            {
                m_nowTimeReactive-=ConstSetting.MAX_INF_TIME;
                //关灯后三秒复活怪物
                m_EnemyMana.Reactive();
                
            }
            //15秒后开灯
            if(m_nowTime>=ConstSetting.SWITCH_ON_TIME)
            {
                m_nowTime-=ConstSetting.SWITCH_ON_TIME;
                m_NextStatus=true;
                OpenLight();
            }
        }
        else if(m_NextStatus==true)//现在在开灯
        {
            m_nowTimeReactive=0;
            //10秒后关灯
            if(m_nowTime>=ConstSetting.SWITCH_OFF_TIME)
            {
                m_nowTime-=ConstSetting.SWITCH_OFF_TIME;
                m_NextStatus=false;
                CloseLight();
            }
        }
    }

    private void OpenLight()
    {
        m_LightControl.gameObject.SetActive(true);
    }
    
    private void CloseLight()
    {
        m_LightControl.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other) 
    {
        if(m_NextStatus==true&&other.gameObject.tag==ObjectTag.MONSTER)
        {

            Ray ray=new Ray(transform.position-Vector3.up,transform.forward*2.5f);
            Debug.DrawRay(transform.position-Vector3.up,transform.forward*2.5f,Color.red,5.0f);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,ConstSetting.MAX_LIGHT_RAYCAST_DISTANCE))
            {
                
                if(hit.collider.gameObject.tag==ObjectTag.MONSTER)
                {
                    // Debug.Log(hit.collider.gameObject.name);
                    hit.collider.gameObject.GetComponent<MonsterDamage>().Damage(ConstSetting.DAMAGE_UNIT);
                }
            }
            
        }
    }


    public void SwitchStatu()
    {
        
        //切换灯的状态
        m_NextStatus=!m_NextStatus;
        m_nowTime=ConstSetting.INIT_TIME;
        m_nowTimeReactive=ConstSetting.INIT_TIME;
        if(m_NextStatus)
        {
            OpenLight();
        }
        else
        {
            CloseLight();
        }
    }
}

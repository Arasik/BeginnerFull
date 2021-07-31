using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private bool HasWeapon=false;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private AudioSource m_AudioSource;
    private Vector3 m_Movement;
    private Quaternion m_Rotation = Quaternion.identity;
    private float m_MoveSpeedRatio=1.0f;
    public float m_turnSpeed =ConstSetting.TURN_SPEED;
    private float m_Open=ConstSetting.INIT_TIME;
    private bool m_OpenBoxKeyDown=false;
    private bool m_SwitchControl=false;
    public GameObject m_Bullet;
    public BulletManager m_BulletManager;
    public Transform m_OpenFirePosition;
    public GameObject m_Gun;
    public AudioClip m_WalkAudio;
    public GameObject m_ShootCircleUI;
    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
    }

    void Move ()
    {
        float horizontal = Input.GetAxis (KeySetting.HORIZONTAL);
        float vertical = Input.GetAxis (KeySetting.VERTICAL);

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();
        m_Movement*=m_MoveSpeedRatio;

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool (PlayerAniPara.IS_WALKING, isWalking);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }
        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, m_turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }
    private void Update()
    {
        Move();
        m_Open+=Time.deltaTime;
        m_Open=m_Open>ConstSetting.SHOOT_SPEED?ConstSetting.SHOOT_SPEED:m_Open;
        
        m_ShootCircleUI.GetComponent<ShootCircleUI>().UpdateUI(m_Open);
        if(m_Open==ConstSetting.SHOOT_SPEED)
        {
            m_ShootCircleUI.SetActive(false);
        }
        else
        {
            m_ShootCircleUI.SetActive(true);
        }
        if(m_Open>=ConstSetting.SHOOT_SPEED&&HasWeapon==true&&Input.GetMouseButtonDown(1))
        {
            m_Animator.SetBool(PlayerAniPara.IS_FIRE,true);
            StartCoroutine(ReFire());
            StartCoroutine(Shoot());
        }
        if(Input.GetButtonDown(KeySetting.OPENBOX))
        {
            m_OpenBoxKeyDown=true;
        }
        if(Input.GetButtonUp(KeySetting.OPENBOX))
        {
            m_OpenBoxKeyDown=false;
        }
        if(Input.GetButtonDown(KeySetting.SWITCH_CONTROL))
        {
            m_SwitchControl=true;
        }
        if(Input.GetButtonUp(KeySetting.SWITCH_CONTROL))
        {
            m_SwitchControl=false;
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        //判断tag
        
        if(other.collider.gameObject.tag==ObjectTag.TREASURE)//碰到宝箱时
        {
            //玩家按下打开箱子的按钮
            if(m_OpenBoxKeyDown)
            {
                m_OpenBoxKeyDown=false;
                m_Animator.SetBool(PlayerAniPara.IS_OPEN,true);
                StartCoroutine(ReOpen());
                other.collider.gameObject.GetComponent<WoodBox>().SetTrigger();
            }
        }
    }
    IEnumerator ReOpen()
    {
        yield return new WaitForSeconds(0.2f);
       m_Animator.SetBool(PlayerAniPara.IS_OPEN,false);
       m_Animator.SetBool(PlayerAniPara.HAS_WEAPON,true);
       HasWeapon=true;
       m_Gun.SetActive(true);
       
    }
    IEnumerator ReFire()
    {
        yield return new WaitForSeconds(ConstSetting.DELAY_CALL_TIME_01);
        m_Animator.SetBool(PlayerAniPara.IS_FIRE,false);

    }
//接触开关判断是否按下按键
    private void OnTriggerStay(Collider other) {
        //判断tag
        if(other.gameObject.tag==ObjectTag.SWITCH)
        {
            //判断按键
            if(m_SwitchControl)
            {
                m_SwitchControl=false;
                //射线判断前方是不是开关,防止隔墙按开关的情况
                Ray ray=new Ray(transform.position,other.gameObject.transform.position-transform.position);
                Debug.DrawRay(transform.position,other.gameObject.transform.position-transform.position,Color.red,5.0f);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit))
                {
                    
                    if(hit.collider.gameObject==other.gameObject)
                    {
                        //因为玩家获取不到灯光的状态所以需要把自己的animator传过去
                        other.gameObject.GetComponent<SwitchControl>().SwitchLight(m_Animator);
                    }
                }
                
            }
        }
    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(ConstSetting.DELAY_CALL_TIME_01);

        //获取鼠标的位置
        Vector3 Point=Vector3.zero;
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit02;//检测碰到的点

        if(Physics.Raycast(mousePos, out hit02,ConstSetting.MAX_RAYCAST_DISTANCE,LayerMask.GetMask(ConstSetting.LAYER_MASK_DEFAULT)))//函数是对射线碰撞的检测
        {
            Point = hit02.point;//得到碰撞点的坐标
        }
        // Point.y=ConstSetting.FIX_HEIGHT_Y;//固定Y轴
        Vector3 m_temp=(Point-transform.position);
        m_temp.y=0;
        m_Rotation = Quaternion.LookRotation(m_temp);

        // GameObject go=Instantiate(m_Bullet,m_OpenFirePosition.position,Quaternion.identity);
        GameObject go=m_BulletManager.GetBullet();
        go.transform.position=m_OpenFirePosition.position;
        go.transform.rotation=Quaternion.identity;
        go.GetComponent<Rigidbody>().velocity=(Point- m_OpenFirePosition.transform.position)*ConstSetting.SHOOT_SPEED;
        
        //射线检测
        // Ray ray=new Ray(m_OpenFirePosition.position,(Point- m_OpenFirePosition.transform.position)*ConstSetting.MAX_RAYCAST_DISTANCE);
        // RaycastHit hit;
        // Debug.DrawRay(m_OpenFirePosition.position,(Point- m_OpenFirePosition.transform.position)*ConstSetting.MAX_RAYCAST_DISTANCE,Color.red,5.0f);
        // if(Physics.Raycast(ray,out hit))
        // {
            // Debug.Log(hit.collider.gameObject.transform.parent.gameObject.name+" "+hit.collider.gameObject.transform.parent.gameObject.tag);
            
            // if(hit.collider.gameObject.tag==ObjectTag.MONSTER)
            // {
            //     hit.collider.gameObject.GetComponent<MonsterDamage>().Damage(ConstSetting.DAMAGE_UNIT);
            // }
            // if(hit.collider.gameObject.transform.parent&&hit.collider.gameObject.transform.parent.gameObject.tag==ObjectTag.MONSTER)
            // {
            //     hit.collider.transform.parent.gameObject.GetComponent<MonsterDamage>().Damage(ConstSetting.DAMAGE_UNIT);
            // }
        // }
        m_Open=ConstSetting.INIT_TIME;
    }

    public void ChangeMoveRatio(float Speed)
    {
        m_MoveSpeedRatio*=Speed;
    }
}
using UnityEngine;

public class MonsterDamage : MonoBaseUnit
{
    private Vector3 InitPosition;
    private Quaternion InitRotaion; 
    public EnemiesManager EnemyMana;
    private float CollisionTime;
    public float SafeTime=ConstSetting.SAFE_TIME;
    object[] m_damage =new object[2];//给予的伤害
    private void Awake() {
        // 获取初始位置方便重生
        InitPosition=transform.position;
        InitRotaion=transform.rotation;
        m_damage[1]=tag;
    }
    private void Update() {
        
        if(SafeTime>=0.0f)
        {
            SafeTime-=Time.deltaTime;
        }
    }
    public override void Damage(float damage)
    {
        m_HP-=damage;
        m_HP=m_HP<ConstSetting.MIN_HP?ConstSetting.MIN_HP:m_HP;
        if(m_HP<=ConstSetting.MIN_HP)
        {
            // 如果怪物死亡就调用相应方法
            MonsterDie();
        }
    }

    private void MonsterDie()
    {
        // Debug.Log("Stop is call");
        // 死亡之后把自己的类型和位置传递到Manager
        gameObject.SetActive(false);
        if(!EnemyMana.EnemyList.Contains(gameObject))
        {
            EnemyMana.EnemyList.Add(gameObject);
        }
    }
    public void init()
    {
        //初始化怪物
        m_HP=m_FullHP;
        transform.position = InitPosition;
        transform.rotation = InitRotaion;
        SafeTime=ConstSetting.SAFE_TIME;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.collider.gameObject.tag==ObjectTag.PLAYER&&SafeTime<=ConstSetting.INIT_TIME)
        {
            m_damage[0]=ConstSetting.DAMAGE_UNIT;
            other.collider.gameObject.GetComponent<PlayerLogic>().Damage(m_damage);
            CollisionTime=ConstSetting.INIT_TIME;
        }
    }
    private void OnCollisionStay(Collision other) 
    {
        if(other.collider.gameObject.tag==ObjectTag.PLAYER&&SafeTime<=ConstSetting.INIT_TIME)
        {
            CollisionTime+=Time.deltaTime;
            if(CollisionTime>=ConstSetting.DAMAGE_INTERAL_TIME)
            {
                CollisionTime-=ConstSetting.DAMAGE_INTERAL_TIME;
                m_damage[0]=ConstSetting.DAMAGE_UNIT;
                other.collider.gameObject.GetComponent<PlayerLogic>().Damage(m_damage);
            }
        }
    }
// 碰撞退出时
    private void OnCollisionExit(Collision other)
    {
        if(other.collider.gameObject.tag==ObjectTag.PLAYER)
        {
            CollisionTime=ConstSetting.INIT_TIME;
            m_damage[0]=ConstSetting.DAMAGE_NULL;
            other.collider.gameObject.GetComponent<PlayerLogic>().Damage(m_damage);
        }
    }
}

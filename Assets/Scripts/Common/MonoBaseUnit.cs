using UnityEngine;

public abstract class MonoBaseUnit : MonoBehaviour
{
    protected float m_HP=ConstSetting.MIN_HP;//当前血量
    protected float m_FullHP=ConstSetting.MAX_HP;//满血
    public virtual void Damage()
    {
        m_FullHP-=ConstSetting.DAMAGE_UNIT;
    }
    public virtual void Damage(float m_damage)
    {
        m_FullHP-=m_damage;
    }
    public virtual void Damage(object[] m_obj)
    {
        
    }
    public delegate void OnDamage(float m_HP,float m_FullHP);
    public static event OnDamage OnDamageEvent;
    protected virtual void MultiCastEvents()
    {
        if (OnDamageEvent != null)
            OnDamageEvent(m_HP, m_FullHP);
    }
}

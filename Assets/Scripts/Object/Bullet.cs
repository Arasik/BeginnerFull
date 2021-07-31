using UnityEngine;

public class Bullet : MonoBehaviour
{  
    public BulletManager m_BulletManager;
    public GameObject m_ParticleSystem;
    private void OnCollisionEnter(Collision other) {
        // Debug.Log(other.collider.name);
        MonsterDamage m_MonsterDamage=other.collider.GetComponent<MonsterDamage>();
        GameObject go02= Instantiate(m_ParticleSystem,transform.position,Quaternion.identity);
        go02.GetComponent<ParticleSystem>().Play();
        go02.GetComponent<AudioSource>().Play();
        if(m_MonsterDamage)
        {
            m_MonsterDamage.Damage(ConstSetting.DAMAGE_UNIT);
        }
        m_BulletManager.CollectBullet(gameObject);
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }
}

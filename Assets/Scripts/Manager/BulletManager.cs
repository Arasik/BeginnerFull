using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletManager:MonoBehaviour
{
    private List<GameObject> m_Bullets=new List<GameObject>();
    [SerializeField]
    private int m_Bullets_Len;
    public GameObject m_Bullet;
    
    private void Start() {
        m_Bullets_Len=m_Bullets.Count;
    }
    public GameObject GetBullet()
    {
        GameObject m_go=null;
        if(m_Bullets.Count>0)
        {
            m_go= m_Bullets[m_Bullets_Len-1];
            m_Bullets.RemoveAt(m_Bullets_Len-1);
            m_go.SetActive(true);
            m_Bullets_Len--;
        }
        if(m_go==null)
        {
            m_go=Instantiate(m_Bullet);
            m_go.GetComponent<Bullet>().m_BulletManager=this;
        }
        return m_go;
    }

    public void CollectBullet(GameObject m_go)
    {
        m_Bullets.Add(m_go);
        m_Bullets_Len++;
    }

}
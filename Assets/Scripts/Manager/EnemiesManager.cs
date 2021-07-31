using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemiesManager : MonoBehaviour
{
    // public List<EnemyInfo> EnemyList=new List<EnemyInfo>();
    public List<GameObject> EnemyList=new List<GameObject>();

    public static EnemiesManager instance;

    public EnemiesManager getInstance()
    {
        if(instance==null)
        {
            instance=new EnemiesManager();
        }
        return instance;
    }

    public void Reactive()
    {
        for(int i=0;i<EnemyList.Count;i++)
        {
            EnemyList[i].SetActive(true);
            EnemyList[i].GetComponent<MonsterDamage>().init();
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpike : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==ObjectTag.PLAYER)
        {
            //减速
            other.gameObject.GetComponent<PlayerMovement>().ChangeMoveRatio(ConstSetting.SLOW_SPEED_RATIO);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag==ObjectTag.PLAYER)
        {
            //离开后加速
            other.gameObject.GetComponent<PlayerMovement>().ChangeMoveRatio(ConstSetting.NORMAL_SPEED_RATIO);
        }
    }
    
}

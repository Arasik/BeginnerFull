using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBox : MonoBehaviour
{
    private Animator animator;

    public GameObject Treasure;
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    public void SetTrigger()
    {
        animator.SetBool(WoodBoxAniPara.IS_OPEN,true);
        GetComponent<BoxCollider>().isTrigger=true;
    }

}

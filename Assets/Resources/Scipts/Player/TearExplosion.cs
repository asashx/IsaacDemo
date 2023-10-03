using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearExplosion : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo info;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        info = animator.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime >= 1.0f)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}

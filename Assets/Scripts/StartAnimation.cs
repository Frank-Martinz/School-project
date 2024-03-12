using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    public Animator anim;
    public AnimationClip a; 

    public void StartAnim() 
    {
        anim = GetComponent<Animator>();
        anim.Play(a.name);
    }
}

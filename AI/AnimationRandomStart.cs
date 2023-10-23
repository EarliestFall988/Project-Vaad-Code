using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationRandomStart : MonoBehaviour
{
    public Animator animator;

    public void Start()
    {
        animator.SetFloat("RandomStart", Random.Range(0f, 1f));
    }
}

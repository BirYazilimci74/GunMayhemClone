using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator swordAnimator;

    public static AnimationController Instance {get; private set;}

    private void Awake()
    {
        if (Instance is not null && Instance == this)
        {
            return;
        }
        Instance = this;
    }

    public void AttackAnimation()
    {
        swordAnimator.SetTrigger("Attack");
    }
}

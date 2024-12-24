using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject dedector;
    
    private static readonly int IsProspect = Animator.StringToHash("isProspect");

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Soil"))
        {
            animator.SetBool(IsProspect, true);
            dedector.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Soil"))
        {
            animator.SetBool(IsProspect, false);
            dedector.SetActive(false);
        }
    }
}

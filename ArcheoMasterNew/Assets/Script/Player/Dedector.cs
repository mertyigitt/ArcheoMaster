using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dedector : MonoBehaviour
{
    [SerializeField] private GameObject dedectorSignal;
    public GameObject DedectorSignal => dedectorSignal;
    
    [SerializeField] private Animator playerAnimator;
    public Animator PlayerAnimator => playerAnimator;
}

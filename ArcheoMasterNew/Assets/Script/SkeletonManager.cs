using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    [SerializeField] private StackMachineFromPlayer stackMachineFromPlayer;
    [SerializeField] private GameObject[] ghostSkeletons;
    [SerializeField] private GameObject[] trueSkeletons;

    private void Update()
    {
        BoneCheck();
    }

    private void BoneCheck()
    {
        if (stackMachineFromPlayer.ObjectStack.Count > 0 && stackMachineFromPlayer.ObjectStack.Count < 11)
        {
            for (int i = 0; i <= stackMachineFromPlayer.ObjectStack.Count - 1; i++)
            {
                trueSkeletons[i].SetActive(true);
            }

            for (int i = 0; i <= stackMachineFromPlayer.ObjectStack.Count - 1; i++)
            {
                ghostSkeletons[i].SetActive(false);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StackToPlayer : MonoBehaviour
{
    [SerializeField] private StackMachineFromPlayer stackMachineFromPlayer;
    [SerializeField] private ObjectChangerMachine objectChangerMachine;
    [SerializeField] private bool isStackMachine;
    [SerializeField] private float moveWaitTime;
   
    private bool _isMoving;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerStack playerStack))
        {
            if (isStackMachine)
            {
                if(stackMachineFromPlayer.ObjectStack.Count > 0 && !_isMoving)
                {
                    _isMoving = true;
                    var obj = stackMachineFromPlayer.ObjectStack.Pop();
                    playerStack.StackObject(obj.GetComponent<Collider>());
                    StartCoroutine(ObjectWaitTime(moveWaitTime));
                }
            }
            else
            {
                if (objectChangerMachine.ObjectStack.Count > 0 && !_isMoving)
                {
                    _isMoving = true;
                    var obj = objectChangerMachine.ObjectStack.Pop();
                    playerStack.StackObject(obj.GetComponent<Collider>());
                    StartCoroutine(ObjectWaitTime(moveWaitTime));
                }
            }
        }
    }
    
    private IEnumerator ObjectWaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isMoving = false;
    }
}

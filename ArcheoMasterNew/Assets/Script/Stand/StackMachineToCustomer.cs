using System.Collections;
using UnityEngine;

public class StackMachineToCustomer : MonoBehaviour
{
    [SerializeField] private StackMachineFromPlayer stackMachineFromPlayer;
    private bool _isMoving;
    

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out CustomerController customerController) && !_isMoving)
        {
            if(stackMachineFromPlayer.ObjectStack.Count > 0)
            {
                _isMoving = true;
                var obj = stackMachineFromPlayer.ObjectStack.Pop();
                StartCoroutine(MoveToTargetPosition(obj, customerController.gameObject.transform.position , customerController));
                StartCoroutine(ObjectWaitTime(2f));
                
            }
        }
    }
    
    private IEnumerator MoveToTargetPosition(GameObject obj, Vector3 targetPosition, CustomerController customerController)
    {
        yield return new WaitForSeconds(1f);
        customerController.isTrade = true;
        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, 5 * Time.deltaTime);
            yield return null;
        }

        obj.transform.position = targetPosition;
        
        Destroy(obj);
    }

    private IEnumerator ObjectWaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isMoving = false;
    }
}

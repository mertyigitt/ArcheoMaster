using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StackMachineFromPlayer : MonoBehaviour
{
    [SerializeField] private int maxStackSize;
    [SerializeField] private Transform stackPosition;
    [SerializeField] private float moveWaitTime;
    [SerializeField] private ObjectType objectType;
    
    private Stack<GameObject> _objectStack = new Stack<GameObject>();
    private bool _isMoving;

    public Stack<GameObject> ObjectStack => _objectStack;

    [Header("Grid Settings")]
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 3;
    [SerializeField] private int layers = 2;
    [SerializeField] private float stackOffset;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerStack playerStack))
        {
            if (_objectStack.Count < maxStackSize)
            {
                if (!_isMoving && playerStack.ObjectStack.Count > 0)
                {
                    GameObject obj = FindAndPopCube(playerStack);
                    if (obj != null)
                    {
                        _isMoving = true;
                        _objectStack.Push(obj);
                        obj.transform.SetParent(gameObject.transform);
                        StartCoroutine(MoveToPosition(obj));
                        StartCoroutine(ObjectWaitTime(moveWaitTime));
                        obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
                        TextChanger.Instance.onCapacityTextChange?.Invoke();
                        playerStack.RearrangeStack();
                    }
                }
            }
        }
    }

    private GameObject FindAndPopCube(PlayerStack playerStack)
    {
        List<GameObject> tempList = new List<GameObject>();
        GameObject targetObj = null;

        while (playerStack.ObjectStack.Count > 0)
        {
            var currentObj = playerStack.ObjectStack.Pop();
            
            if (targetObj == null && currentObj.TryGetComponent(out StackableObject stackableObject) && stackableObject.ObjectType == objectType)
            {
                targetObj = currentObj;
            }
            else
            {
                tempList.Add(currentObj);
            }
        }
        
        for (int i = tempList.Count - 1; i >= 0; i--)
        {
            playerStack.ObjectStack.Push(tempList[i]);
        }

        return targetObj;
    }

    private IEnumerator MoveToPosition(GameObject obj)
    {
        int index = _objectStack.Count - 1;

        int row = index % rows;
        int column = (index / rows) % columns;
        int layer = index / (rows * columns);

        float layerOffset = 1.5f;

        Vector3 targetPosition = stackPosition.localPosition + new Vector3(column * 1.5f, layer * layerOffset, row * 1.5f);

        yield return MoveToTargetPosition(obj, targetPosition);

        obj.transform.localPosition = targetPosition;
        obj.transform.localScale = Vector3.one;
    }

    private IEnumerator MoveToTargetPosition(GameObject obj, Vector3 targetPosition)
    {
        while (Vector3.Distance(obj.transform.localPosition, targetPosition) > 0.1f)
        {
            obj.transform.localPosition = Vector3.Lerp(obj.transform.localPosition, targetPosition, 5 * Time.deltaTime);
            yield return null;
        }

        obj.transform.localPosition = targetPosition;
    }

    private IEnumerator ObjectWaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isMoving = false;
    }
}

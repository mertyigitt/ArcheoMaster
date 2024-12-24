using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChangerMachine : MonoBehaviour
{
    [SerializeField] private StackMachineFromPlayer stackMachineFromPlayer;
    [SerializeField] private float moveWaitTime;
    [SerializeField] private Transform stackPosition;
    [SerializeField] private GameObject outObject;
    [SerializeField] private int maxStackSize;
    
    
    private Stack<GameObject> _objectStack = new Stack<GameObject>();
    private bool _isMoving;
    
    public Stack<GameObject> ObjectStack => _objectStack;
    
    [Header("Grid Settings")]
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 3;
    [SerializeField] private int layers = 2;
    [SerializeField] private float stackOffset;

    private void Update()
    {
        if (stackMachineFromPlayer.ObjectStack.Count > 0 && _objectStack.Count < maxStackSize && !_isMoving)
        {
            _isMoving = true;
            var obj =stackMachineFromPlayer.ObjectStack.Pop();
            obj.SetActive(false);
            var instObj = Instantiate(outObject, transform.position, transform.rotation);
            _objectStack.Push(instObj);
            instObj.transform.SetParent(gameObject.transform);
            StartCoroutine(MoveToPosition(instObj, stackPosition));
            StartCoroutine(ObjectWaitTime(moveWaitTime));
            instObj.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }
    
    private IEnumerator MoveToPosition(GameObject obj , Transform tarPos)
    {
        var index = _objectStack.Count - 1;

        var row = index % rows;
        var column = (index / rows) % columns;
        var layer = index / (rows * columns);

        var layerOffset = 1.5f;

        var targetPosition = tarPos.localPosition + new Vector3(column * 1.5f, layer * layerOffset, row * 1.5f);

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

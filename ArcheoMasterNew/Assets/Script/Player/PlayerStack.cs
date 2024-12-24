using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStack : MonoBehaviour
{
    [SerializeField] private Transform stackPosition;
    [SerializeField] private PlayerStatsSO playerStats;
    
    private Stack<GameObject> _objectStack = new Stack<GameObject>();
    private Transform _lastpos;
    
    public Stack<GameObject> ObjectStack => _objectStack;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out StackableObject _))
        {
            StackObject(other);
        }
    }
    

    public void StackObject(Collider obj)
    {
        if (_objectStack.Count < playerStats.CargoCapacity)
        {
            if(_objectStack.Count > 0)
            {
                var lastElement = _objectStack.ToArray()[0];
                _lastpos = lastElement.transform;
            }
            _objectStack.Push(obj.gameObject);
            
            obj.gameObject.transform.SetParent(transform);
            if (_objectStack.Count == 1)
            {
                StartCoroutine(MoveToPosition(obj.gameObject, stackPosition.localPosition));
            }
            else
            {
                StartCoroutine(MoveToPosition(obj.gameObject, _lastpos.localPosition + new Vector3(0, 1.5f, 0)));
            }
            obj.gameObject.GetComponent<Collider>().enabled = false;
            
            obj.gameObject.transform.rotation = new Quaternion(0,0,0,0);
            TextChanger.Instance.onCapacityTextChange?.Invoke();
        }
    }
    
    public void RearrangeStack()
    {
        Vector3 currentPosition = stackPosition.localPosition;

        
        GameObject[] objects = _objectStack.ToArray();
        for (int i = objects.Length - 1; i >= 0; i--)
        {
            var obj = objects[i];
            StartCoroutine(MoveToPosition(obj, currentPosition)); 
            currentPosition += new Vector3(0, 1.5f, 0); 
        }
    }
    
    private IEnumerator MoveToPosition(GameObject obj, Vector3 targetPosition)
    {
        while (Vector3.Distance(obj.transform.localPosition, targetPosition) > 0.01f)
        {
            obj.transform.localPosition = Vector3.Lerp(obj.transform.localPosition, targetPosition, 5 * Time.deltaTime);
            yield return null; 
        }
        
        obj.transform.localPosition = targetPosition;
    }
}

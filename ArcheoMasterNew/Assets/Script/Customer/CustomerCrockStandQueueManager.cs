using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomerCrockStandQueueManager : MonoBehaviour
{
    public static CustomerCrockStandQueueManager Instance;
    
    public Transform queueStartPoint;
    [SerializeField] private float spacing = 1.5f;
    private Queue<CustomerController> _customersQueue = new Queue<CustomerController>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void JoinQueue(CustomerController customer)
    {
        if (customer == null)
        {
            Debug.LogError("Sıraya katılmak isteyen müşteri null!", this);
            return;
        }

        _customersQueue.Enqueue(customer);
        UpdateQueuePositions();
    }

    public void LeaveQueue(CustomerController customer)
    {
        if (_customersQueue.Contains(customer))
        {
            _customersQueue = new Queue<CustomerController>(_customersQueue.Where(c => c != customer));
            UpdateQueuePositions();
        }
    }

    private void UpdateQueuePositions()
    {
        int index = 0;
        foreach (var customer in _customersQueue)
        {
            Vector3 targetPosition = GetQueuePosition(index);
            customer.SetTarget(targetPosition);
            customer.agent.isStopped = false;
            index++;
        }
    }

    private Vector3 GetQueuePosition(int index)
    {
        if (queueStartPoint == null)
        {
            Debug.LogError("queueStartPoint atanmadı!", this);
            return Vector3.zero;
        }
        return queueStartPoint.position + (index * spacing * queueStartPoint.up);
    }
}


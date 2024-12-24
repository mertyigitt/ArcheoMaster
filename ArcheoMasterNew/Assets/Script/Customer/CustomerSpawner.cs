using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private List<GameObject> customers;
    
    
    void Start()
    {
        InvokeRepeating(nameof(CustomerSpawn), 100, 35);
    }

    void CustomerSpawn()
    {
        if(customers.Count > 0)
            customers = customers.Where(obj => obj != null).ToList();
        
        if (customers.Count < 5)
        {
            var customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
            customers.Add(customer);
        }
    }
}

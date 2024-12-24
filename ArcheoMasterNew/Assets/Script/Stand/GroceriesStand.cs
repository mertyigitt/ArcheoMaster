using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceriesStand : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private Transform spawnStartPoint; 
    [SerializeField] private int gridRows = 3; 
    [SerializeField] private int gridColumns = 3;
    [SerializeField] private int gridLevels = 2;
    [SerializeField] private float spacing = 1.5f;
    [SerializeField] private float levelHeight = 2.0f; 
    
    
    private List<Vector3> _spawnPoints = new List<Vector3>();
    private HashSet<Vector3> _occupiedPoints = new HashSet<Vector3>();
    
    private void Start()
    {
        GenerateSpawnPoints();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CustomerController customerController))
        {
            customerController.isBuy = true;
            SpawnObjects();
        }
    }

    private void GenerateSpawnPoints()
    {
        for (int level = 0; level < gridLevels; level++)
        {
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridColumns; col++)
                {
                    Vector3 point = spawnStartPoint.position 
                                    + new Vector3(col * spacing, level * levelHeight, row * spacing);
                    _spawnPoints.Add(point);
                }
            }
        }
    }

    private void SpawnObjects()
    {
        foreach (Vector3 point in _spawnPoints)
        {
            if (!_occupiedPoints.Contains(point))
            {
                GameObject newObject = Instantiate(objectPrefab, point, objectPrefab.transform.rotation);
                newObject.GetComponent<StackMoney>().groceriesStand = this;
                _occupiedPoints.Add(point);
                break;
            }
        }
    }

    public void RemoveObject(Vector3 point)
    {
        if (_occupiedPoints.Contains(point))
        {
            _occupiedPoints.Remove(point); // Noktayı boş olarak işaretle
        }
    }
    
}

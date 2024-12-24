using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoilInstantiateManager : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private int objectCount;
    [SerializeField] private GameObject[] instantiateObjects;
    [SerializeField] private List<GameObject> objects;
    
    private Vector3 _areaSize;

    private void Start()
    {
        _areaSize = boxCollider.size;
        InstantiateObject(instantiateObjects[Random.Range(0, instantiateObjects.Length)]);
        InvokeRepeating(nameof(NullCheck) , 0f, 5f);
    }

    private void Update()
    {
        if (objects.Count == 0)
        {
            InstantiateObject(instantiateObjects[Random.Range(0, instantiateObjects.Length)].gameObject);
        }
    }

    public void InstantiateObject(GameObject instObject)
    {
        for (int i = 0; i < objectCount; i++)
        {
            var randomPosition = new Vector3(
                Random.Range(-_areaSize.x / 2, _areaSize.x / 2),
                3,
                Random.Range(-_areaSize.z / 2, _areaSize.z / 2));
            var spawnPosition = transform.position + transform.rotation * randomPosition;
            var obj = Instantiate(instObject, spawnPosition, instObject.transform.rotation);
            objects.Add(obj);
            obj.transform.SetParent(transform);
        }
    }
    
    private void NullCheck()
    {
        objects = objects.Where(obj => obj != null).ToList();
    }
}

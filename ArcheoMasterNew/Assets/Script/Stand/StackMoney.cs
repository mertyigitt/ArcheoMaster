using UnityEngine;
using Random = UnityEngine.Random;

public class StackMoney : MonoBehaviour
{
    public GroceriesStand groceriesStand;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController playerController))
        {
            playerController.money += 50;
            groceriesStand.RemoveObject(transform.position);
            TextChanger.Instance.onMoneyTextChange?.Invoke(playerController.money);
            GetComponent<Rigidbody>().isKinematic = false;
            RandomForce();
            Destroy(gameObject,2f);
        }
    }
    
    private void RandomForce()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomDirection = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(0.5f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;
            
            rb.AddForce(randomDirection * 250);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unlocker : MonoBehaviour
{
    [SerializeField] private int unlockPrice;
    [SerializeField] private int spendValue;
    [SerializeField] private TextMeshProUGUI unlockPriceText;
    [SerializeField] private float spendTime;
    [SerializeField] private Image unlockImage;
    [SerializeField] private GameObject spendObject;
    [SerializeField] private GameObject unlockObject;
    [SerializeField] private GameObject dirtySiteObject;
    
    private int _totalSpend;
    private bool _spendable;
    private GameObject _lastMoney;
    

    private void Start()
    {
        unlockPriceText.text = unlockPrice.ToString();
        unlockImage.fillAmount = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerController playerController) && !_spendable && unlockPrice > 0)
        {
            _spendable = true;
            StartCoroutine(Unlock(playerController));
        }

        if (unlockPrice <= 0)
        {
            unlockObject.SetActive(true);
            Destroy(dirtySiteObject);
            if (_lastMoney is not null)
            {
                Destroy(_lastMoney);
            }
        }
    }

    private IEnumerator Unlock(PlayerController playerController)
    {
        yield return new WaitForSeconds(spendTime); 
        _lastMoney = Instantiate(spendObject, playerController.gameObject.transform.position + new Vector3(0,2.5f,0), Quaternion.identity);
        StartCoroutine(MoveToTargetPosition(_lastMoney, transform.position + new Vector3(0,-2,0)));
        
        playerController.money -= spendValue;
        unlockPrice -= spendValue;
        _totalSpend += spendValue;
        unlockPriceText.text = unlockPrice.ToString();
        TextChanger.Instance?.onMoneyTextChange?.Invoke(playerController.money);
        unlockImage.fillAmount = (float)_totalSpend / (float)(_totalSpend + unlockPrice);
        _spendable = false;
    }
    
    private IEnumerator MoveToTargetPosition(GameObject obj, Vector3 targetPosition)
    {
        while (Vector3.Distance(obj.transform.localPosition, targetPosition) > 0.1f)
        {
            obj.transform.localPosition = Vector3.Lerp(obj.transform.localPosition, targetPosition, 10 * Time.deltaTime);
            yield return null;
        }
    
        obj.transform.localPosition = targetPosition;
        Destroy(obj,1f);
    }
}

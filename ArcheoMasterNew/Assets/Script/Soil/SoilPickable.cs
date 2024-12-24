using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class SoilPickable : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float dedectorTime;
    [SerializeField] private GameObject instantiateObject;

    private float _time;

    private void Start()
    {
        slider.maxValue = dedectorTime;
        slider.value = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Dedector dedector))
        {
            dedector.DedectorSignal.SetActive(true);
            slider.gameObject.SetActive(true);
            _time += Time.deltaTime;
            slider.value = _time;
            if (_time >= dedectorTime)
            { 
                gameObject.GetComponent<Collider>().enabled = false;
                CloseObject(dedector);
                dedector.PlayerAnimator.Play("Dig");
                StartCoroutine(InstantiateObject());
                Destroy(gameObject,3f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Dedector dedector))
        {
            CloseObject(dedector);
        }
    }

    private IEnumerator InstantiateObject()
    {
        yield return new WaitForSeconds(2);
        Instantiate(instantiateObject, transform.position, instantiateObject.transform.rotation);
    }
    
    private void CloseObject(Dedector dedector)
    {
        dedector.DedectorSignal.SetActive(false);
        _time = 0;
        slider.gameObject.SetActive(false);
    }
}

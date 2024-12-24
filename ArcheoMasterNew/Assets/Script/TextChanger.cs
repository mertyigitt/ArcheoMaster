using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextChanger : MonoBehaviour
{
    public static TextChanger Instance;
    
    [SerializeField] TextMeshProUGUI capacityText;
    [SerializeField] PlayerStack playerStack;
    [SerializeField] TextMeshProUGUI moneyText;
    
    public UnityAction onCapacityTextChange = delegate { };
    public UnityAction<int> onMoneyTextChange = delegate { };
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        onCapacityTextChange += OnCapacityTextChange;
        onMoneyTextChange += OnMoneyTextChange;
    }

    private void OnDisable()
    {
        onCapacityTextChange -= OnCapacityTextChange;
        onMoneyTextChange -= OnMoneyTextChange;
    }

    private void OnCapacityTextChange()
    {
        capacityText.text = playerStack.ObjectStack.Count.ToString();
    }
    private void OnMoneyTextChange(int money)
    {
        moneyText.text = money.ToString();
    }
}

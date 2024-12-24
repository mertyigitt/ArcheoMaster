using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private int cargoCapacity;
    
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    
    public int CargoCapacity
    {
        get => cargoCapacity;
        set => cargoCapacity = value;
    }
}

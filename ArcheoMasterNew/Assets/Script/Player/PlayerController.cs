using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Animator animator;

    private CharacterController _characterController;
    private Vector3 _velocity;
    
    private static readonly int Speed = Animator.StringToHash("MoveSpeed");
    private static readonly int İsProspect = Animator.StringToHash("isProspect");
    
    public int money;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        TextChanger.Instance.onMoneyTextChange?.Invoke(money);
    }

    void Update()
    {
        Move();
        Rotation();
    }

    private void Move()
    {
        _velocity.z = joystick.Vertical;
        _velocity.x = joystick.Horizontal;
        if (animator.GetBool(İsProspect))
        {
            _characterController.Move(_velocity * (playerStats.Speed / 2 * Time.deltaTime));
        }
        else
        {
            _characterController.Move(_velocity * (playerStats.Speed * Time.deltaTime));
        }
        
        animator.SetFloat(Speed, _velocity.magnitude);
    }
    
    private void Rotation()
    {
        var movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        var targetDirection = Vector3.RotateTowards(_characterController.transform.forward, movementDirection,
            15 * Time.deltaTime, 0f);
        _characterController.transform.rotation = Quaternion.LookRotation(targetDirection);
    }
}

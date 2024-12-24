using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandState : IState
{
    private readonly NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private CustomerJarStandQueueManager _queueManager;
    private Animator _animator;
    private static readonly int Walk = Animator.StringToHash("Walk");

    public StandState(NavMeshAgent agent, Animator animator)
    {
        _agent = agent;
        _queueManager = CustomerJarStandQueueManager.Instance;
        _animator = animator;
    }

    public void OnEnter()
    {
        if (_queueManager != null)
        {
            _queueManager.JoinQueue(_agent.GetComponent<CustomerController>());
        }

        _agent.GetComponent<CustomerController>().currentTarget = CustomerJarStandQueueManager.Instance.queueStartPoint.position;
        _animator.SetBool(Walk, true);
        
    }

    public void Tick()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                _agent.isStopped = true; 
                _animator.SetBool(Walk, false);
            }
        }
    }

    public void OnExit()
    {
        if (_queueManager != null)
        {
            _queueManager.LeaveQueue(_agent.GetComponent<CustomerController>());
        }
    }

    public void SetTarget(Vector3 newTarget)
    {
        _targetPosition = newTarget;
        if (_agent != null)
        {
            _agent.SetDestination(_targetPosition);
        }
    }
}

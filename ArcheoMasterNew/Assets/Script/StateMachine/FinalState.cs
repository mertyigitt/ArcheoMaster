using UnityEngine;
using UnityEngine.AI;

public class FinalState : IState
{
    private readonly NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private Animator _animator;
    private static readonly int Walk = Animator.StringToHash("Walk");

    public FinalState(NavMeshAgent agent, Animator animator)
    {
        _agent = agent;
        _animator = animator;
    }

    public void OnEnter()
    {
        _targetPosition = new Vector3(-40, _agent.gameObject.transform.position.y, -40);
        _agent.isStopped = false;
        _agent.SetDestination(_targetPosition);
        _animator.SetBool(Walk, true);
    }

    public void Tick()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                _agent.isStopped = true; 
                Object.Destroy(_agent.gameObject);
            }
        }
    }

    public void OnExit()
    {
        
    }
    
}

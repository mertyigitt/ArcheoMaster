using UnityEngine;
using UnityEngine.AI;


public class CustomerController : MonoBehaviour
{
    private StandState _standState;
    private StateMachine _stateMachine; 
    private Animator _animator;
    
    [HideInInspector] public Vector3 currentTarget;
    
    public NavMeshAgent agent;
    public bool isTrade;
    public bool isBuy;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent bulunamadı!", this);
        }
    }

    private void Start()
    {
        _standState= new StandState(agent,_animator);
        GroceriesState groceriesState = new GroceriesState(agent,_animator);
        FinalState finalState = new FinalState(agent,_animator);
        
        _stateMachine.AddState(_standState, groceriesState, () => isTrade);
        _stateMachine.AddState(groceriesState, finalState, () => isBuy);
        _stateMachine.SetState(_standState);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    public void SetTarget(Vector3 newTarget)
    {
        currentTarget = newTarget;
        _standState.SetTarget(currentTarget);
    }
}

using UnityEngine;
using UnityEngine.AI;

public class Crocodile : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    [Header("Movement")]
    [SerializeField] private float _wanderRadius = 15f;
    [SerializeField] private float _wanderJitter = 5f;
    private float _idleTimer;
    private float _timeToWait;
    [SerializeField] private float _minWaitTimeWhenStuck;
    [SerializeField] private float _maxWaitTimeWhenStuck;

    public Animator Animator { get { return _animator; } private set { _animator = value; } }
    public NavMeshAgent NavMeshAgent { get { return _navMeshAgent; } private set { _navMeshAgent = value; } }
    public float IdleTimer { get { return _idleTimer; } set { _idleTimer = value; } }
    public float TimeToWait { get { return _timeToWait; } set { _timeToWait = value; } }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    void Update()
    {
     
    }


    /// <summary>
    /// Finds a new destination biased towards the crocodile's forward direction.
    /// </summary>
    public void SetNewWanderDestination()
    {
        Vector3 wanderCircleCenter = transform.position + transform.forward * Random.Range(_wanderRadius/2, _wanderRadius);

        Vector2 randomPoint = Random.insideUnitCircle * _wanderJitter;
        Vector3 destination = wanderCircleCenter + new Vector3(randomPoint.x, 0, randomPoint.y);

        if (NavMesh.SamplePosition(destination, out NavMeshHit navHit, _wanderRadius, -1))
        {
            _navMeshAgent.SetDestination(navHit.position);
        }
        else
        {
            SetRandomFallbackDestination();
        }
        _idleTimer = 0f;
        _timeToWait = Random.Range(_minWaitTimeWhenStuck, _maxWaitTimeWhenStuck);
    }

    private void SetRandomFallbackDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _wanderRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, _wanderRadius, -1))
        {
            _navMeshAgent.SetDestination(navHit.position);
        }
    }

    public void PlayAnimation(string animationState)
    {
        _animator.Play(animationState);
    }
}

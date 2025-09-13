using UnityEngine;
using UnityEngine.AI;

public class Crocodile : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private CrocodileManager _crocodileManager;

    [Header("Movement")]
    [SerializeField] private float _wanderRadius = 15f;
    [SerializeField] private float _wanderJitter = 5f;
    private float _idleTimer;
    private float _timeToWait;
    [SerializeField] private float _minWaitTimeWhenIdle;
    [SerializeField] private float _maxWaitTimeWhenIdle;

    public Animator Animator { get { return _animator; } private set { _animator = value; } }
    public NavMeshAgent NavMeshAgent { get { return _navMeshAgent; } private set { _navMeshAgent = value; } }
    public CrocodileManager CrocodileManager { get { return _crocodileManager; } set { _crocodileManager = value; } }
    public float IdleTimer { get { return _idleTimer; } private set { _idleTimer = value; } }
    public float TimeToWait { get { return _timeToWait; } private set { _timeToWait = value; } }

    public void Initialize(CrocodileManager manager)
    {
        _crocodileManager = manager;
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        transform.Rotate(0, Random.Range(0, 360), 0);
        _timeToWait = Random.Range(_minWaitTimeWhenIdle, _maxWaitTimeWhenIdle);
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
        ResetIdleTimer();
    }

    public void ResetIdleTimer()
    {
        _idleTimer = 0f;
        _timeToWait = Random.Range(_minWaitTimeWhenIdle, _maxWaitTimeWhenIdle);
    }

    public void IncrementIdleTimer()
    {
        _idleTimer += Time.deltaTime;
    }

    /// <summary>
    /// Finds a new destination unbiased towards the corcodiles' forward direction.
    /// </summary>
    private void SetRandomFallbackDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _wanderRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, _wanderRadius, -1))
        {
            _navMeshAgent.SetDestination(navHit.position);
        }
    }

    /// <summary>
    /// Plays the specified animation in this crocodile's animator component.
    /// </summary>
    /// <param name="animationState"></param>
    public void PlayAnimation(string animationState)
    {
        _animator.Play(animationState);
    }

    public void ApproachBar()
    {
        Debug.Log(NavMeshAgent);
        Debug.Log(_crocodileManager);
        Debug.Log(_crocodileManager.Bar);
        NavMeshAgent.SetDestination(_crocodileManager.Bar.transform.position);
    }

    public void LookAtPlayer()
    {
        transform.LookAt(Camera.main.transform);
    }
}

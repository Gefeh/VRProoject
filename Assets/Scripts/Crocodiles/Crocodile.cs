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
    [SerializeField] private float _minWaitTimeWhenIdle = 1.5f;
    [SerializeField] private float _maxWaitTimeWhenIdle = 4f;
    [SerializeField] private Transform _spawnPoint;

    [Header("Customer")]
    [SerializeField] private float _satisfaction;
    [SerializeField] private float _maxSatisfaction = 100f;
    [SerializeField] private float _thirstThreshold = 50f;
    [SerializeField] private float _thirstCoefficient = 1f;

    [Header("Combat")]
    [SerializeField] public bool nearPlayer;

    public Animator Animator { get { return _animator; } private set { _animator = value; } }
    public NavMeshAgent NavMeshAgent { get { return _navMeshAgent; } private set { _navMeshAgent = value; } }
    public CrocodileManager CrocodileManager { get { return _crocodileManager; } set { _crocodileManager = value; } }
    public float IdleTimer { get { return _idleTimer; } private set { _idleTimer = value; } }
    public float TimeToWait { get { return _timeToWait; } private set { _timeToWait = value; } }
    public float Satisfaction { get { return _satisfaction; } private set { _satisfaction = value; } }
    public float MaxSatisfaction { get { return _maxSatisfaction; } private set { _maxSatisfaction = value; } }
    public float ThirstThreshold { get { return _thirstThreshold; } private set { _thirstThreshold = value; } }
    public Transform SpawnPoint { get { return _spawnPoint; } private set { _spawnPoint = value; } }

    public void Initialize(CrocodileManager manager, Transform spawnPoint)
    {
        _crocodileManager = manager;
        _spawnPoint = spawnPoint;
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        transform.Rotate(0, Random.Range(0, 360), 0);
        _timeToWait = Random.Range(_minWaitTimeWhenIdle, _maxWaitTimeWhenIdle);
        _satisfaction = Random.Range(_thirstThreshold, _maxSatisfaction);
        _thirstCoefficient = Random.Range(0.8f, 1.2f);
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

    public void ReduceSatisfaction()
    {
        _satisfaction -= _thirstCoefficient * Time.deltaTime;
    }

    public void Satisfy(float percentage = 100)
    {
        _satisfaction = _maxSatisfaction * 1.01f;

        _thirstCoefficient *= Random.Range(1f, 1.15f);

        if (Random.value < 0.1f)
        {
            _crocodileManager.SpawnCrocodiles(1);
        }
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

    public void ReturnToSpawn()
    {
        _navMeshAgent.SetDestination(new Vector3(_spawnPoint.position.x, transform.position.y, _spawnPoint.position.z));
        _idleTimer = 10f;
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
        NavMeshAgent.SetDestination(_crocodileManager.Bar.transform.position);
    }

    public void LookAtPlayer()
    {
        Transform playerTransform = Camera.main.transform;

        Vector3 targetPosition = new Vector3(playerTransform.position.x,
                                             this.transform.position.y,
                                             playerTransform.position.z);

        transform.LookAt(targetPosition);
    }

    public void DealDamage()
    {
        if (nearPlayer)
        {
            _crocodileManager.player.TakeDamage();
        }
    }

    public void ApproachPlayer()
    {
        Transform playerTransform = Camera.main.transform;
        NavMeshAgent.SetDestination(playerTransform.position);
    }
}

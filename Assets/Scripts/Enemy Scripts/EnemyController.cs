using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 3;
    public float speedRun = 5;
    private float chaseCooldown = 1.2f;
    private float currentCooldown;

 
    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1.0f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;
    public float attackRange = 1f;
    
    private Rigidbody Rb;
    private EnemyAnimation enemyAnimation;
    public Transform[] waypoints;
    
    int m_CurrentWaypointIndex;
 
    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;
 
    float m_WaitTime;
    float m_TimeToRotate;
    bool m_playerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;
 
    void Start()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_playerInRange = false;
        m_PlayerNear = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;
        Rb = GetComponent<Rigidbody>();
 
        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();
 
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        
    }
 
    private void Update()
    {
        EnviromentView();
        if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 1)
        {
            Rb.isKinematic = true;
            
        }
        else
        {
            Rb.isKinematic = false;
            
        }
        if (!m_IsPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        if (m_CaughtPlayer)
        {
            enemyAnimation.SetAttack();
        }
        else if (!m_IsPatrol)
        {
            enemyAnimation.SetRun();
        }
        else if (navMeshAgent.velocity.magnitude > 0)
        {
            enemyAnimation.SetWalk();
        }
        else
        {
            enemyAnimation.SetIdle();
        }
    }
    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);

            float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            if (distanceToPlayer <= 1.0f)
            {
                Stop();
                m_CaughtPlayer = true;
                currentCooldown = chaseCooldown;
                enemyAnimation.SetAttack();
                //Attack function here
            }
        }
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
                // Only stop attacking if the player is beyond a certain distance.
                if (distanceToPlayer > attackRange)
                {
                    m_CaughtPlayer = false;
                }
                else
                {
                    // Reset the cooldown to stay in attack mode
                    currentCooldown = chaseCooldown;
                }
            }
        }
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                    Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }
 
    private void Patroling()
    {
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }
 
    private void OnAnimatorMove()
    {
 
    }
 
    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
 
    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }
 
    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }
 
    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }
 
    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }
 
    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
 
        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_playerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_playerInRange = false;
            }
            if (m_playerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }
    }
}
 
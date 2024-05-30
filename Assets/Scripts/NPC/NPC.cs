using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public bool enemy;



    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private Transform playerPos;
    [HideInInspector] public Vector3 defaultPos;
    [HideInInspector] public Animator anim;
    [HideInInspector] public float distanceToPlayer;

    public float initialAggroRange;
    public float maxAggroRange;
    public float chaseSpeed;
    public float chaseAngSpeed;
    public float chaseAccel;
    public float chaseStopDist;

    private float defSpeed;
    private float defAng;
    private float defAcc;
    private float defStop;

    public NavMeshAgent Agent
    {
        get => agent;
    }
    
    public Pathing pathing;
    private static readonly int Speed = Animator.StringToHash("Speed");

    public void ChangeAgentSpeed(float speed, float angularSpeed, float acceleration, float stoppingDist)
    {
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        agent.stoppingDistance = stoppingDist;
    }

    public void ResetAgentSpeed()
    {
        agent.speed = defSpeed;
        agent.angularSpeed = defAng;
        agent.acceleration = defAcc;
        agent.stoppingDistance = defStop;
    } 
    
    void Start()
    {
        defaultPos = transform.position;
        
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        playerPos = PlayerManager.instance.player.transform;
        
        defSpeed = agent.speed;
        defAng = agent.angularSpeed;
        defAcc = agent.acceleration;
        defStop = agent.stoppingDistance;
        anim = GetComponentInChildren<Animator>();
        
        stateMachine.Initialise();
    }
    
    void Update()
    {
        if (anim != null)
        {
            anim.SetFloat(Speed,agent.velocity.magnitude);
        }

        if (enemy)
        {
            distanceToPlayer = Vector3.Distance(playerPos.position, transform.position);

            if (distanceToPlayer <= initialAggroRange)
            {
                stateMachine.ChangeState(stateMachine.chaseState);
            }
        }
    }


    //Aggro range debug
    private void OnDrawGizmosSelected()
    {

        if (!enemy) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,initialAggroRange);
        Gizmos.color = Color.yellow;
        if (Application.isPlaying) Gizmos.DrawWireSphere(defaultPos,maxAggroRange);
        else Gizmos.DrawWireSphere(transform.position,maxAggroRange);
        
        
       
    }
}

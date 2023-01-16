using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private PlayerHealth playHP;
    private Transform playerTransform;
    public override void Enter()
    {
        playHP = PlayerManager.instance.player.GetComponent<PlayerHealth>();
        playerTransform = playHP.transform;
        
        enemy.ChangeAgentSpeed(enemy.chaseSpeed,enemy.chaseAngSpeed,enemy.chaseAccel,enemy.chaseStopDist);
        if (enemy.anim != null)
        {
            enemy.anim.speed = 2;
        }
    }
  
    public override void Perform()
    {
        float distanceFromStart = Vector3.Distance(playerTransform.position, enemy.defaultPos);
        
        if (distanceFromStart >= enemy.maxAggroRange)
        {
            StateMachine.ChangeState(StateMachine.patrolState);
        }
        else
        {
            ChasePlayer();
        
            if (enemy.distanceToPlayer <= enemy.chaseStopDist)
            {
                FacePlayer();
            }
        }
    }

    public override void Exit()
    {
        
    }

    public void ChasePlayer()
    {
        enemy.Agent.SetDestination(playerTransform.position);
    }

    private void FacePlayer()
    {
        Vector3 direction = (playerTransform.position - enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
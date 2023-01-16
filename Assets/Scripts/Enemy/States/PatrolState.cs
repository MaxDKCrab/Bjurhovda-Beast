using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
  public int waypointIndex;
  public float waitTimer;
  public override void Enter()
  {
    enemy.ResetAgentSpeed();
    if (enemy.anim != null)
    {
      enemy.anim.speed = 1;
    }
    enemy.Agent.SetDestination(enemy.pathing.waypoints[waypointIndex].position);
  }
  
  public override void Perform()
  {
    PatrolCycle();
  }
  
  public override void Exit()
  {
    
  }

  public void PatrolCycle()
  {
    if (enemy.Agent.remainingDistance < 0.2f)
    {
      
        if (waypointIndex < enemy.pathing.waypoints.Count -1)
        {
          waypointIndex++;
        }
        else
        {
          waypointIndex = 0;
        }

        enemy.Agent.SetDestination(enemy.pathing.waypoints[waypointIndex].position);
    }
  }
}

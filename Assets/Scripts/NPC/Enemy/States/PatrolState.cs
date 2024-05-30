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
    NPC.ResetAgentSpeed();
    if (NPC.anim != null)
    {
      NPC.anim.speed = 1;
    }
    NPC.Agent.SetDestination(NPC.pathing.waypoints[waypointIndex].position);
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
    if (NPC.Agent.remainingDistance < 0.2f)
    {
      
        if (waypointIndex < NPC.pathing.waypoints.Count -1)
        {
          waypointIndex++;
        }
        else
        {
          waypointIndex = 0;
        }

        NPC.Agent.SetDestination(NPC.pathing.waypoints[waypointIndex].position);
    }
  }
}

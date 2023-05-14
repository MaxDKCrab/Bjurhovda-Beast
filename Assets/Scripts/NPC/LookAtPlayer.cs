using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject playerRef;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerRef = PlayerManager.instance.player;
    }

    
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerRef.transform.position, transform.position);
        
        if (distanceToPlayer <= agent.stoppingDistance)
        {
            FacePlayer();
        }
    }
    
    
    private void FacePlayer()
    {
        Vector3 direction = (playerRef.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}

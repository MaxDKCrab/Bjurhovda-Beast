using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    
    public BaseState activeState;
    public PatrolState patrolState;
    public ChaseState chaseState;
    

    public void Initialise()
    {
        patrolState = new PatrolState();
        chaseState = new ChaseState();
        ChangeState(patrolState);
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (activeState != null)
        {
            activeState.Exit();
        }

        activeState = newState;

        if (activeState != null)
        {
            activeState.StateMachine = this;
            activeState.NPC = GetComponent<NPC>();
            activeState.Enter();
        }

    }
    
}

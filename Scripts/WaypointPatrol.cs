using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    int m_CurrentWaypointIndex;
    public Transform[] waypoints;

    //minor edit variables
    private bool isWaiting = false;
    private float waitTime; //make random from .5 secs to 2 secs

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);

    }

    void Update()
    {
       //minor edit: 50% chance that at each waypoint the ghost stops for a random time
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            float randomChance = Random.value;
            if (randomChance <= 0.5f) //50% chance
            {
                isWaiting = true;
                waitTime = Random.Range(0.5f, 2f); //wait random time bewteen .5 secs and 2 secs
                navMeshAgent.isStopped = true;
            }
            else
            {
                GoToNextDestination();
            }
        }

        if(isWaiting)
        {
            waitTime -= Time.deltaTime;
            if(waitTime <= 0f)
            {
                isWaiting=false;
                navMeshAgent.isStopped=false;
                GoToNextDestination();
            }
        }
    }

    //minor edit: moving to next waypoint is its own function
    void GoToNextDestination()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
}

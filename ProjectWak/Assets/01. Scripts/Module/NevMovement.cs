using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour
{
    private NavMeshAgent navAgent = null;

    private float moveSpeed = 3f;

    public bool IsStopped => navAgent.isStopped;
    public bool IsArrived => (navAgent.pathPending == false) && navAgent.remainingDistance <= navAgent.stoppingDistance;

    public void Move(Vector3 destination)
    {
        navAgent.SetDestination(destination);
    }

    public void StopImmediately()
    {
        Move(transform.position);
    }

    public void SetActive(bool active)
    {
        navAgent.isStopped = active;
        navAgent.enabled = !active;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
        navAgent.speed = moveSpeed;
    }
}

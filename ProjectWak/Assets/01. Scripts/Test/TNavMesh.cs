using UnityEngine;
using UnityEngine.AI;

public class TNavMesh : MonoBehaviour
{
	[SerializeField] Transform target;

    private NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    [ContextMenu("SetDestination")]    
    public void SetDestination()
    {
        navAgent.SetDestination(target.position);
    }
}

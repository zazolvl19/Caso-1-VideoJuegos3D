using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float minDistanceToAttack = 2.0f;

    NavMeshAgent _navAgent;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= minDistanceToAttack)
        {
            Attack();
        }
        else
        {
            _navAgent.SetDestination(target.position);
        }
    }

    private void Attack()
    {

        HealthController playerHealth = target.GetComponent<HealthController>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(50); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class ZombieBehavior : MonoBehaviour
{

    public float speed = 1;
    public Transform Target;

    public Animator anim;

    public float attackDistance = 3;

    public float attackRate = 1;

    public NavMeshAgent navMeshAgent;

    void ChaseTarget()
    {
        if (Target != null && Target.gameObject.activeSelf)
        {
            navMeshAgent.SetDestination(Target.position);
            anim.SetBool("chase", true);
        }
        else
        {
            anim.SetBool("chase", false);
        }
    }

    IEnumerator CheckTargetLocation()
    {
        while (true)
        {
            ChaseTarget();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void Start()
    {
        StartCoroutine(CheckTargetLocation());
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, Target.position) < attackDistance)
        {
            navMeshAgent.isStopped = true;
        }
        else
        {
            navMeshAgent.isStopped = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, navMeshAgent.stoppingDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, Target.position);
    }


    public void Stop()
    {
        StopCoroutine(CheckTargetLocation());
        if (navMeshAgent != null)
            navMeshAgent.isStopped = true;
    }

    public void OnDisable()
    {
        Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MovingBetweenPoints : MonoBehaviour
{

    public List<Transform> points = new List<Transform>();
    public List<NavMeshAgent> Agents = new List<NavMeshAgent>();


    void Start() => StartCoroutine(RandomMoveAgents(0f));

    IEnumerator RandomMoveAgents(float randomTime)
    {

        yield return new WaitForSeconds(randomTime);

        for (int i = 0; i < Agents.Count; i++)
        {
            float random = Random.Range(0f, 1f);
            int randomPoint = Random.Range(0, points.Count);

            // if (random <= 0.75f)
            // {
            if (Agents[i].isStopped)
            {
                Debug.Log("setting agent position" + points[randomPoint].position);

                Agents[i].SetDestination(points[randomPoint].position);
                Agents[i].GetComponent<Animator>().SetBool("moving", true);
                Agents[i].isStopped = false;
            }
            // }
        }

        float nextRandomTime = Random.Range(5f, 10f);

        StartCoroutine(RandomMoveAgents(nextRandomTime));
    }

    void Update()
    {
        foreach (var x in Agents)
        {
            if (x.remainingDistance <= 1f)
            {
                x.GetComponent<Animator>().SetBool("moving", false);
                x.isStopped = true;
            }
        }
    }


    void OnDrawGizmosSelected()
    {

        foreach (var x in Agents)
        {
            if (x.isStopped)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(x.transform.position, 1f);
            Gizmos.DrawLine(x.transform.position, x.destination);
        }
    }
}

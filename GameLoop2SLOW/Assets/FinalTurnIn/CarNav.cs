using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNav : MonoBehaviour
{
    [SerializeField] private List<Transform> movePositions = new List<Transform>();
    private NavMeshAgent m_Agent;
    private Transform currentDestination;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        currentDestination = RandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, currentDestination.position);
        if (dist < 1.2f)
        {
            currentDestination = RandomDestination();
        }

        // Apply the movement with Time.deltaTime
        m_Agent.destination = currentDestination.position;
    }

    private Transform RandomDestination()
    {
        if (movePositions.Count > 0)
        {
            int rd = Random.Range(0, movePositions.Count);
            return movePositions[rd];
        }
        return null;
    }
}

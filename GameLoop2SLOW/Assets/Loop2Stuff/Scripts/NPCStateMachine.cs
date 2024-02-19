using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // this makes sure a rb is on whatever object attatched, else wont function
public class NPCStateMachine : MonoBehaviour
{
    public Transform playerTransform; //tracks player
    public float maxFollowDistance = 5.0f;// how far it'll let the player move away
    public float stoppingDistance = 2.0f;// how far it stops form the player
    public float moveSpeed = 2.0f;

    public float lungeDistance = 7.0f; // how far it lunges
    public float lungeCooldown = 2.0f; // how long the cooldown is

    private enum State //sets up state machine
    {
        Idle,
        MoveTowards,
        MaintainDistance,
        TooClose,
        Rush,
        LungeCooldown
    }

    private State currentState = State.Idle; //starting state
    private Rigidbody rb; //rigidbody
    private Vector3 lastPosition; //smoothing
    private float lungeTimer = 0.0f; //sets timer

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        Debug.Log(currentState);
        switch (currentState)
        {
            case State.Idle:
                if (IsPlayerInLineOfSight())
                {
                    currentState = State.MoveTowards;
                }
                break;

            case State.MoveTowards:
                if (distanceToPlayer <= stoppingDistance)
                {
                    currentState = State.MaintainDistance;
                }
                else
                {
                    Vector3 direction = (playerTransform.position - transform.position).normalized;
                    rb.velocity = direction * moveSpeed;

                    // Orient NPC to look at player
                    transform.LookAt(playerTransform.position);
                }
                break;
            case State.MaintainDistance:
                if (distanceToPlayer > stoppingDistance && distanceToPlayer <= maxFollowDistance)
                {
                    currentState = State.MoveTowards;
                }
                else if (distanceToPlayer < stoppingDistance)
                {
                    currentState = State.TooClose;
                }
                break;

            case State.TooClose:
                if (lungeTimer <= 0.0f)
                {
                    currentState = State.Rush;
                }
                else
                {
                    lungeTimer -= Time.deltaTime;
                }
                break;
            case State.Rush:
                Vector3 rushDirection = (playerTransform.position - transform.position).normalized;
                rb.velocity = rushDirection * moveSpeed * 2.0f;
                if (distanceToPlayer < lungeDistance)
                {
                    currentState = State.LungeCooldown;
                    lungeTimer = lungeCooldown;
                }
                break;

            case State.LungeCooldown:
                if (lungeTimer <= 0.0f)
                {
                    currentState = State.MoveTowards; // Transition back to MoveTowards state after cooldown
                }
                else
                {
                    lungeTimer -= Time.deltaTime;
                }
                break;
        }

        lastPosition = transform.position;
    }

    private bool IsPlayerInLineOfSight() // raycasting to see if player is in line of sight
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit, maxFollowDistance))
        {
            if (hit.transform == playerTransform)
            {
                return true;
            }
        }
        return false;
    }
}

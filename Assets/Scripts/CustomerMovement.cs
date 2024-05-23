using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CustomerMovement : MonoBehaviour
{
    public delegate void Movement();
    public static Movement CustomerExit;

    private NavMeshAgent agent;
    private GameObject waypoints;
    private GameObject activeWaypoint;

    private void OnEnable()
    {
        CustomerOrder.OrderComplete += MoveToExit;
    }
    private void OnDisable()
    {
        CustomerOrder.OrderComplete -= MoveToExit;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waypoints = GameObject.Find("Waypoints");
        activeWaypoint = waypoints.transform.GetChild(2).gameObject;
        agent.SetDestination(activeWaypoint.transform.position);
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            var q = Quaternion.LookRotation(waypoints.transform.GetChild(3).position - transform.position, Vector3.up);
            gameObject.GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, q, 3f));
        }
    }
    private void MoveToExit()
    {
        agent.SetDestination(waypoints.transform.GetChild(1).position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Exit")
        {
            Debug.Log("Customer left");
            CustomerExit.Invoke();
            Destroy(gameObject);
        }
    }
}

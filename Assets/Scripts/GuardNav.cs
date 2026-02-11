using UnityEngine;
using UnityEngine.AI;

public class GuardNav : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPointIndex = 0;
    private NavMeshAgent agent;

    bool isInAngle, isInRange, isNotHidden;
    public GameObject Player;
    public float DetectRange = 10;
    public float DetectAngle = 45;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(patrolPoints[currentPointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        isInAngle = false;
        isInRange = false;
        isNotHidden = false;

        if (Vector3.Distance(transform.position, Player.transform.position) < DetectRange)
        {
            isInRange = true;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (Player.transform.position - transform.position), out hit, Mathf.Infinity))
        {
            if (hit.transform == Player.transform)
            {
                isNotHidden = true;
            }
        }

        Vector3 side1 = Player.transform.position - transform.position;
        Vector3 side2 = transform.forward;
        float angle = Vector3.SignedAngle(side1, side2, Vector3.up);
        if (angle < DetectAngle && angle < (-1 * DetectAngle))
        {
            isInAngle = true;
        }

        if (!isInAngle || !isInRange || !isNotHidden)
        {
            if (!agent.pathPending && agent.remainingDistance < 3.0f)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPointIndex].position);
            }
        } else
        {
            agent.SetDestination(Player.transform.position);
        }
    }
}

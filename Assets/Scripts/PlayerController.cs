using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;

    float x = 0;

    private void Start()
    {
        agent.updateRotation = false;
    }

    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        */

        x = Mathf.MoveTowards(x, SimpleInput.GetAxis("Horizontal"), Time.deltaTime * 10f);

        Vector3 target = new Vector3(x, 0, 1);
        character.Move(target, false, false);

        /*
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
        */
    }
}

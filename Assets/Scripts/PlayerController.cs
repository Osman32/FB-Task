using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private NavMeshAgent agent;
    private ThirdPersonCharacter character;

    private float x = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();

        agent.updateRotation = false;
        character.m_MoveSpeedMultiplier = speed / 10f;
    }

    void Update()
    {
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

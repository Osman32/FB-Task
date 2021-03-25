using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;

    private void Start()
    {
        agent.updateRotation = false;
        NavMesh.avoidancePredictionTime = 0.5f;

    }

    void Update()
    {
        agent.SetDestination(player.position);

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }

    }
}

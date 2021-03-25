using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Transform player;
    private NavMeshAgent agent;
    private ThirdPersonCharacter character;
    private Vector3 randomOffset = Vector3.zero;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();

        agent.updateRotation = false;
        character.m_MoveSpeedMultiplier = speed / 10f;

        randomOffset = new Vector3(Random.Range(-5f, 5f), 0, 0);

        StartCoroutine(RandomCoroutine());
    }

    private void Update()
    {
        agent.SetDestination(player.position + randomOffset);

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }

    }

    private IEnumerator RandomCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            if ((transform.position.z - player.position.z) > 3f)
            {
                randomOffset = new Vector3(Random.Range(-5f, 5f), 0, 0);
            }
            else
            {
                randomOffset = Vector3.zero;
            }
        }

    }
}

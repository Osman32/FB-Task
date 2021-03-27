using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;
    public float speed = 10f;

    [SerializeField] SkinnedMeshRenderer meshRenderer;

    private Animator anim;
    private Transform player;
    private NavMeshAgent agent;
    private ThirdPersonCharacter character;
    private Vector3 randomOffset = Vector3.zero;
    private bool isDead;

    #region Private Methods
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        anim = GetComponent<Animator>();

        agent.updateRotation = false;
        character.m_MoveSpeedMultiplier = speed / 10f;

        randomOffset = new Vector3(Random.Range(-5f, 5f), 0, 0);

        StartCoroutine(RandomCoroutine());
    }
    private void Update()
    {
        if (health > 0)
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
        else
        {
            if (!isDead)
            {
                isDead = true;
                agent.enabled = false;
                character.Move(Vector3.zero, false, false);
                anim.SetTrigger("DeathTrigger");
                Die();
            }
        }

        if (Vector3.Distance(transform.position, player.position) < 1.5f)
        {
            StartCoroutine(player.GetComponent<PlayerController>().Dead());
        }
    }
    private IEnumerator RandomCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            if ((transform.position.z - player.position.z) > 2f)
            {
                randomOffset = new Vector3(Random.Range(-5f, 5f), 0, 0);
            }
            else
            {
                randomOffset = Vector3.zero;
            }
        }

    }
    private void Die()
    {
        Material[] mats = meshRenderer.materials;
        mats[0].DOFloat(1, "_Cutoff", 1f);
        meshRenderer.materials = mats;
        GetComponent<Rigidbody>().isKinematic = true;
        GameManager.Instance.enemies.Remove(this);
    }
    #endregion
}

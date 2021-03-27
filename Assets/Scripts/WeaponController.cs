using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;
    [Space]
    [Header("Property")]
    public float range = 1f;
    public float fireRate = 1f;
    public float damage = 34f;

    private EnemyController closestEnemy;

    #region Private Methods
    private void Start()
    {
        StartCoroutine(FireCoroutine());
    }

    private void Update()
    {
        Aim();
    }

    private void Fire()
    {
        if (closestEnemy != null && closestEnemy.health > 0)
        {
            closestEnemy.health -= damage;
        }
    }

    private void Aim()
    {
        float minDistance = float.MaxValue;
        Vector3 closestEnemyPos = Vector3.zero;
        foreach (var item in GameManager.Instance.enemies)
        {
            float distance = Vector3.Distance(item.transform.position, transform.position);
            if (range >= distance && distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = item;
                closestEnemyPos = item.transform.position + new Vector3(0f, 3f, 0f);
            }
        }

        leftHand.position = Vector3.Lerp(leftHand.position, closestEnemyPos + new Vector3(-Mathf.Clamp((minDistance / 3f), 0, 1.5f), 0f, 0f), Time.deltaTime * 10f);
        rightHand.position = Vector3.Lerp(rightHand.position, closestEnemyPos + new Vector3(Mathf.Clamp((minDistance / 3f), 0, 1.5f), 0f, 0f), Time.deltaTime * 10f);
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Fire();
        }

    }
    #endregion

}

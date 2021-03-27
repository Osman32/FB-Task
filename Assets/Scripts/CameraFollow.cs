using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 5f;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + distance); //Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z + distance), Time.deltaTime * 10f);
    }
}

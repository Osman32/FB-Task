using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandling : MonoBehaviour
{
    private Animator anim;

    public float rightIKWeight = 1f;
    public float leftIKWeight = 1f;

    [SerializeField] private Transform leftIK;
    [SerializeField] private Transform rightIK;

    [SerializeField] private Transform leftIKTarget;
    [SerializeField] private Transform rightIKTarget;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightIKWeight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightIKTarget.position);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftIKWeight);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftIKTarget.position);

        rightIKTarget.LookAt(rightIK.position);
        rightIKTarget.localEulerAngles = new Vector3(rightIKTarget.localEulerAngles.x, rightIKTarget.localEulerAngles.y + 180f, rightIKTarget.localEulerAngles.z-90f);

        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightIKWeight);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightIKTarget.rotation);

        leftIKTarget.LookAt(leftIK.position);
        leftIKTarget.localEulerAngles = new Vector3(leftIKTarget.localEulerAngles.x, leftIKTarget.localEulerAngles.y + 180f, leftIKTarget.localEulerAngles.z - 270f);

        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftIKWeight);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftIKTarget.rotation);
    }
}

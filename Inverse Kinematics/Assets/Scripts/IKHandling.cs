using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandling : MonoBehaviour
{
    Animator anim;
    public float ikWeight=1;

    public Transform leftIKTarget;
    public Transform rightIKTarget;

    public Transform hintLeft;
    public Transform hintRight;

    public float lookIKWeight;
    public float bodyWeight;
    public float headWeight;
    public float eyesWeight;
    public float clampWeight;

    Vector3 lFPos;
    Vector3 rFPos;

    float lFootWeight;
    float rFootWeight;

    Transform leftFoot;
    Transform rightFoot;


    Quaternion lFRot;
    Quaternion rFRot;
    public Transform lookPos;

    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit leftHit; anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, ikWeight);
        RaycastHit rightHit;

        Vector3 lpos = leftFoot.TransformPoint(Vector3.zero);
        Vector3 rpos = rightFoot.TransformPoint(Vector3.zero);
        if (Physics.Raycast(lpos, -Vector3.up, out leftHit, 0.5f))
        {
            lFPos = leftHit.point;
            lFRot = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
        }
        if (Physics.Raycast(rpos, -Vector3.up, out rightHit, 0.5f))
        {
            rFPos = rightHit.point;
            rFRot = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
        }
    }
    private void OnAnimatorIK()
    {
        lFootWeight = anim.GetFloat("LeftFoot");
        rFootWeight = anim.GetFloat("RightFoot");

        anim.SetLookAtWeight(lookIKWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
        anim.SetLookAtPosition(lookPos.position);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, lFootWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rFootWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftFoot, lFPos + new Vector3(0,offset,0));
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rFPos + new Vector3(0, offset, 0));


        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, lFootWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rFootWeight);

        anim.SetIKRotation(AvatarIKGoal.LeftFoot, lFRot);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rFRot);
        //anim.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, ikWeight);
        //anim.SetIKHintPositionWeight(AvatarIKHint.RightKnee, ikWeight);

        //anim.SetIKHintPosition(AvatarIKHint.LeftKnee, hintLeft.position);
        //anim.SetIKHintPosition(AvatarIKHint.RightKnee, hintRight.position);





    }
}

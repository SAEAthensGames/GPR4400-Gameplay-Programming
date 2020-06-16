using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSnap : MonoBehaviour
{
    public bool useIK;

    public bool leftHandIK;
    public bool rightHandIK;

    public bool leftFootIK;
    public bool rightFootIK;

    public Vector3 leftHandPos;
    public Vector3 rightHandPos;

    public Vector3 leftFootPos;
    public Vector3 rightFootPos;

    public Vector3 leftHandOffset;
    public Vector3 rightHandOffset;

    public Vector3 leftFootOffset;
    public Vector3 rightFootOffset;

    public Quaternion leftHandRot;
    public Quaternion rightHandRot;

    public Quaternion leftFootRot;
    public Quaternion rightFootRot;

    public Quaternion leftFootRotOffset;
    public Quaternion rightFootRotOffset;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        RaycastHit LHit;
        RaycastHit RHit;

        RaycastHit LFHit;
        RaycastHit RFHit;
        if(Physics.Raycast(transform.position +new Vector3(0.0f, 2.0f, 0.5f), -transform.up + new Vector3(-0.5f, 0.0f, 0.0f), out LHit, 1f))
        {
            leftHandIK = true;
            leftHandPos = LHit.point - leftHandOffset;
            leftHandRot = Quaternion.FromToRotation(Vector3.forward, LHit.normal);
        }
        else
        {
            leftHandIK = false;
        }
        if (Physics.Raycast(transform.position + new Vector3(0.0f, 2.0f, 0.5f), -transform.up + new Vector3(0.5f, 0.0f, 0.0f), out RHit, 1f))
        {
            rightHandIK = true;
            rightHandPos = RHit.point - rightHandOffset;
            rightHandRot = Quaternion.FromToRotation(Vector3.forward, RHit.normal);
        }
        else
        {
            rightHandIK = false;
        }
        if (Physics.Raycast(transform.position + new Vector3(-0.5f, 0.4f, 0.0f), transform.forward, out LFHit, 1f))
        {
            leftFootIK = true;
            leftFootPos = LFHit.point - leftFootOffset;
            leftFootRot = (Quaternion.FromToRotation(Vector3.up, LFHit.normal)) * leftFootRotOffset;
        }
        else
        {
            leftFootIK = false;
        }
        if (Physics.Raycast(transform.position + new Vector3(0.5f, 0.4f, 0.0f), transform.forward, out RFHit, 1f))
        {
            rightFootIK = true;
            rightFootPos = RFHit.point - rightFootOffset;
            rightFootRot = (Quaternion.FromToRotation(Vector3.up, RFHit.normal)) * rightFootRotOffset;
        }
        else
        {
            rightFootIK = false;
        }
    }

    private void Update()
    {
        //Left Hand
        Debug.DrawRay(transform.position + new Vector3(0.0f, 2.0f, 0.5f), -transform.up + new Vector3(-0.5f, 0.0f, 0.0f), Color.green);
        //Right Hand
        Debug.DrawRay(transform.position + new Vector3(0.0f, 2.0f, 0.5f), -transform.up + new Vector3(0.5f, 0.0f, 0.0f), Color.green);
        //Left Foot
        Debug.DrawRay(transform.position + new Vector3(-0.5f, 0.4f, 0.0f), transform.forward, Color.red);
        //Right Foot
        Debug.DrawRay(transform.position + new Vector3(0.5f, 0.4f, 0.0f), transform.forward, Color.red);
    }
    private void OnAnimatorIK()
    {
        if (useIK)
        {
            if (leftHandIK)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos);

                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRot);

            }
            if (rightHandIK)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos);

                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandRot);
            }
            if (leftFootIK)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
                anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);

                anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);
            }
            if (rightFootIK)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
                anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);

                anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);
            }
        }
    }
}

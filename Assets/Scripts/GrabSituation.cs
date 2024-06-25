using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSituation : MonoBehaviour
{
    [SerializeField] bool isLeftDetected;
    [SerializeField] bool isRightDetected;

    [SerializeField] bool leftHandGrabbing;
    [SerializeField] bool rightHandGrabbing;



    [SerializeField] OVRHand leftHand;
    [SerializeField] OVRHand rightHand;
   

    [SerializeField] Transform grabObject;
    [SerializeField] Vector3 originGrabObjectPos;
    [SerializeField] Quaternion originGrabObjectRot;

    [SerializeField] bool isGrabbing;
    [SerializeField] Rigidbody grabObjectRb;

    [SerializeField] Vector3 leftHandOffset;
    [SerializeField] Vector3 rightHandOffset;


    [SerializeField] GameObject blueSign;
    [SerializeField] GameObject redSign;



    [SerializeField] Transform cameraRig;  // OVR Camera Rig을 여기에 연결
    [SerializeField] Transform originalPos;

    [SerializeField] float holdTime;                 // 유지 시간
    [SerializeField] float gestureTimer; 



    void Start()
    {
        isGrabbing = false;

        isLeftDetected = false;
        isRightDetected = false;

        leftHandGrabbing = false;
        rightHandGrabbing = false;

        blueSign.SetActive(false);
        redSign.SetActive(false);

        holdTime = 5.0f;
        gestureTimer = 0.0f;


        originGrabObjectPos = grabObject.transform.position;
        originGrabObjectRot = grabObject.transform.rotation;
    }




    void Update()
    {

        leftHandGrabbing = IsHandGrabbing(leftHand);
        rightHandGrabbing = IsHandGrabbing(rightHand);

        if (leftHandGrabbing && rightHandGrabbing)
        {
            if (!isGrabbing && isLeftDetected && isRightDetected)
            {
                StartGrab();

            }
        }
        else
        {
            if (isGrabbing)
            {
                EndGrab();
            }
        }

    }

    private void FixedUpdate()
    {
        if (isGrabbing)
        {
            UpdateGrab();
        }
        else
        {
            EndGrab();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            isLeftDetected = true;
            Debug.Log(other.name + " + left detected");
        }
        if (other.CompareTag("RightHand"))
        {
            isRightDetected = true;
            Debug.Log(other.name + " + right detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftHand")) isLeftDetected = false;
        if (other.CompareTag("RightHand")) isRightDetected = false;
    }


    private bool IsHandGrabbing(OVRHand hand)
    {
        return hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
    }


    private void StartGrab()
    {
        isGrabbing = true;
        grabObjectRb.isKinematic = true; // 물체를 잡을 때 물리 효과 비활성화
        gestureTimer = 0f;  // 타이머 초기화

        // 손과 물체 사이의 상대적인 위치 저장
        leftHandOffset = grabObject.position - leftHand.transform.position;
        rightHandOffset = grabObject.position - rightHand.transform.position;


        blueSign.gameObject.SetActive(true);
        redSign.gameObject.SetActive(false);
    }

    private void UpdateGrab()
    {
        // 손의 위치를 고정
        // 손의 위치를 고정
        Vector3 newLeftHandPosition = grabObject.position - leftHandOffset;
        Vector3 newRightHandPosition = grabObject.position - rightHandOffset;

        // 손의 위치를 설정 (트래킹을 통해 실제 손 위치를 강제로 변경하는 것은 불가능, 시각적 효과 제공)
        leftHand.transform.position = newLeftHandPosition;
        rightHand.transform.position = newRightHandPosition;



        gestureTimer += Time.deltaTime;
        // 설정된 시간 동안 제스처가 유지되면 위치 초기화
        if (gestureTimer >= holdTime)
        {
            cameraRig.position = originalPos.position;
            cameraRig.rotation = originalPos.rotation;
            Debug.Log("Returned to original position after holding gesture.");
            isGrabbing = false;  // 상태 리셋
            gestureTimer = 0f;

            grabObject.position = originGrabObjectPos;
            grabObject.rotation = originGrabObjectRot; 
        }
    }


    private void EndGrab()
    {
        isGrabbing = false;
        grabObjectRb.isKinematic = false; // 물체를 놓을 때 물리 효과 활성화
        gestureTimer = 0f;


        blueSign.gameObject.SetActive(false);
        redSign.gameObject.SetActive(true);

        grabObject.position = originGrabObjectPos;
        grabObject.rotation = originGrabObjectRot;

    }

   

}

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
    [SerializeField] GameObject returnSign;


    [SerializeField] float holdTime;                 // ���� �ð�
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
        returnSign.SetActive(false);

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
        grabObjectRb.isKinematic = true; // ��ü�� ���� �� ���� ȿ�� ��Ȱ��ȭ
        gestureTimer = 0f;  // Ÿ�̸� �ʱ�ȭ

        // �հ� ��ü ������ ������� ��ġ ����
        leftHandOffset = grabObject.position - leftHand.transform.position;
        rightHandOffset = grabObject.position - rightHand.transform.position;


        blueSign.gameObject.SetActive(true);
        redSign.gameObject.SetActive(false);
    }

    private void UpdateGrab()
    {
        // ���� ��ġ�� ����
        // ���� ��ġ�� ����
        Vector3 newLeftHandPosition = grabObject.position - leftHandOffset;
        Vector3 newRightHandPosition = grabObject.position - rightHandOffset;

        // ���� ��ġ�� ���� (Ʈ��ŷ�� ���� ���� �� ��ġ�� ������ �����ϴ� ���� �Ұ���, �ð��� ȿ�� ����)
        leftHand.transform.position = newLeftHandPosition;
        rightHand.transform.position = newRightHandPosition;



        gestureTimer += Time.deltaTime;
        // ������ �ð� ���� ����ó�� �����Ǹ� ��ġ �ʱ�ȭ
        if (gestureTimer >= holdTime)
        {
            Debug.Log("Returned to original position after holding gesture.");
            isGrabbing = false;  // ���� ����
            gestureTimer = 0f;

            grabObject.position = originGrabObjectPos;
            grabObject.rotation = originGrabObjectRot;
            Debug.Log("���� ��ġ�� ����");

            returnSign.SetActive(true);
        }
    }


    private void EndGrab()
    {
        isGrabbing = false;
        grabObjectRb.isKinematic = false; // ��ü�� ���� �� ���� ȿ�� Ȱ��ȭ
        gestureTimer = 0f;


        blueSign.gameObject.SetActive(false);

        grabObject.position = originGrabObjectPos;
        grabObject.rotation = originGrabObjectRot;

    }

   

}

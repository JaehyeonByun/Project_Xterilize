using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSituation : MonoBehaviour
{
    [SerializeField] bool isLeftDetected;
    [SerializeField] bool isRightDetected;

    [SerializeField] bool isLeftGripPressed;
    [SerializeField] bool isRightGripPressed;

    [SerializeField] bool isStartSimulation;

    [SerializeField] Transform leftHandTransform;
    [SerializeField] Transform rightHandTransform;

    private Vector3 leftHandPreviousPosition;
    private Vector3 rightHandPreviousPosition;

    void Start()
    {
        isLeftDetected = false;
        isRightDetected = false;

        isLeftGripPressed = false;
        isRightGripPressed = false;

        isStartSimulation = false;
    }

    void Update()
    {
        isLeftGripPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        isRightGripPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (isLeftDetected && isRightDetected && isLeftGripPressed && isRightGripPressed && !isStartSimulation)
        {
            isStartSimulation = true;
            Debug.Log("Start Simulation");
            Debug.Log("Don't release your hold here");

            leftHandPreviousPosition = leftHandTransform.position;
            rightHandPreviousPosition = rightHandTransform.position;
        }
        else if (isStartSimulation && (!isLeftGripPressed || !isRightGripPressed))
        {
            Debug.Log("End Simulation");
            isStartSimulation = false;
        }

        if (isStartSimulation)
        {
            LockControllerPosition();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            isLeftDetected = true;
            Debug.Log(other.name + " detected");
        }
        if (other.CompareTag("RightHand"))
        {
            isRightDetected = true;
            Debug.Log(other.name + " detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftHand")) isLeftDetected = false;
        if (other.CompareTag("RightHand")) isRightDetected = false;
    }

    private void DebugGripPressed()
    {
        if (isLeftGripPressed)
        {
            Debug.Log("Left GripButton Pressed");
        }
        else
        {
            Debug.Log("Left GripButton depressed");
        }

        if (isRightGripPressed)
        {
            Debug.Log("Right GripButton Pressed");
        }
        else
        {
            Debug.Log("Right GripButton depressed");
        }
    }

    void LockControllerPosition()
    {
        // 그립 버튼이 눌려있을 때 컨트롤러 위치를 고정
        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch); // 진동 추가 (선택사항)
        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch); // 진동 추가 (선택사항)

        // 여기서는 컨트롤러의 Transform을 이전 위치로 설정하여 고정시킵니다.
        leftHandTransform.position = leftHandPreviousPosition;
        rightHandTransform.position = rightHandPreviousPosition;
    }
}

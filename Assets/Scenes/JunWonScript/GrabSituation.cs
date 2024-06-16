using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GrabSituation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool isLeftDetected;
    [SerializeField] bool isRightDetected;

    [SerializeField] InputDevice targetDevice;
    [SerializeField] bool isLeftGripPressed;
    [SerializeField] bool isRightGripPressed;

    [SerializeField] bool isStartSimulation;




    void Start()
    {
        isLeftDetected = false;
        isRightDetected = false;

        isLeftGripPressed = false;
        isRightGripPressed = false;

        // 넘겨받아 고정하고 있는 상황 시작
        isStartSimulation = false;

    }

    // Update is called once per frame
    void Update()
    {
        // OVRInput 계속 좌우 눌러져 있는 지 확인
        isLeftGripPressed = 
            OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        isRightGripPressed =
            OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        //DebugGripPressed();
        
        if (isLeftDetected && isRightDetected &&
            isLeftGripPressed && isRightGripPressed &&
            !isStartSimulation)
        {
            isStartSimulation = true;
            Debug.Log("Start Simulation");
            Debug.Log("Don\'t release your hold here");
        }
        else if(isStartSimulation && (!isLeftGripPressed || !isRightGripPressed))
        {
            Debug.Log("End Simulation");
        }

        
           
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LeftHand") { 
            isLeftDetected = true;
            Debug.Log(other.name + "detected");
        }
        if (other.tag == "RightHand") {
            isRightDetected = true;
            Debug.Log(other.name + "detected");
        };
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LeftHand") isLeftDetected = false;
        if (other.tag == "RightHand") isRightDetected = false;
    }


    // Debug Oculus gripButton bool value 
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


}

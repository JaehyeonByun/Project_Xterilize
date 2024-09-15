using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICotrolGesture : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update
    
    public void Uioff()
    {
        UI.SetActive(false);
    }
}

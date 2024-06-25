using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUiAwake : MonoBehaviour
{
    public GameObject EndUI;
    // Start is called before the first frame update
    void EndUi()
    {
        EndUI.SetActive(true);
    }
}

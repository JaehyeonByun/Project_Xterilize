using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject PlayUI;

    public void OnClick()
    {
        StartUI.SetActive(false);
        PlayUI.SetActive(true);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGestureOff : MonoBehaviour
{
    public GameObject DefaultUI;

    public void OnClick()
    {
        DefaultUI.SetActive(true);
    }
}

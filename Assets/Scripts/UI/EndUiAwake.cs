using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUiAwake : MonoBehaviour
{
    public GameObject EndUI;
    public Material overlayMaterial;

    private void Start()
    {
        Invoke("EndUi", 3.0f);
    }
    void EndUi()
    {
        EndUI.SetActive(true);
        ApplyOverlay();
    }

    public void ApplyOverlay()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            Material[] materials = renderer.materials;

            Material[] newMaterials = new Material[materials.Length + 1];

            for (int i = 0; i < materials.Length; i++)
            {
                newMaterials[i] = materials[i];
            }

            newMaterials[materials.Length] = overlayMaterial;

            renderer.materials = newMaterials;
        }
    }


}

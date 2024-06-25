using UnityEngine;

public class AddOverlay : MonoBehaviour
{
    public Material overlayMaterial;

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



using UnityEngine;

public class AddOverlayOnAwake : MonoBehaviour
{
    public Material overlayMaterial;

    void Awake()
    {
        // Check "Conta" Tag
        if (CompareTag("Conta"))
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
}


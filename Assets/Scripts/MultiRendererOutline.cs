using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRendererOutline : MonoBehaviour
{
    public Color outlineColor = Color.red;
        public float outlineWidth = 0.03f;

    private List<Renderer> renderers = new List<Renderer>();
    private List<Material[]> originalMaterials = new List<Material[]>();
    private List<Material[]> outlinedMaterials = new List<Material[]>();

    private Material outlineMaterial;
    private bool isActive = false;

    void Awake()
    {
        // Find ALL renderers in this object and children
        renderers.AddRange(GetComponentsInChildren<Renderer>());

        // Create outline material
        outlineMaterial = new Material(Shader.Find("Outlined/Uniform"));
        outlineMaterial.SetColor("_OutlineColor", outlineColor);
        outlineMaterial.SetFloat("_Outline", outlineWidth);

        // Cache original materials for every renderer
        foreach (Renderer rend in renderers)
        {
            originalMaterials.Add(rend.materials);

            // Build outlined array (original + outline material)
            Material[] mats = new Material[rend.materials.Length + 1];
            for (int i = 0; i < rend.materials.Length; i++)
                mats[i] = rend.materials[i];

            mats[mats.Length - 1] = outlineMaterial;
            outlinedMaterials.Add(mats);
        }
    }

    public void EnableOutline()
    {
        if (isActive) return;
        isActive = true;

        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].materials = outlinedMaterials[i];
        }
    }

    public void DisableOutline()
    {
        if (!isActive) return;
        isActive = false;

        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].materials = originalMaterials[i];
        }
    }

    public void ToggleOutline()
    {
        if (isActive) DisableOutline();
        else EnableOutline();
    }
}

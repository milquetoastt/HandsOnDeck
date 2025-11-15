using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class WarningOutline: MonoBehaviour
{
    public Color outlineColor = Color.red;
    public float outlineWidth = 4f;

    private Material baseMaterial;
    private Material outlineMaterial;
    private Renderer rend;
    private bool isActive = false;

    void Awake()
    {
        rend = GetComponent<Renderer>();

        // Save original material
        baseMaterial = rend.material;

        // Create outline material
        outlineMaterial = new Material(Shader.Find("Outlined/Uniform"));
        outlineMaterial.SetColor("_OutlineColor", outlineColor);
        outlineMaterial.SetFloat("_Outline", outlineWidth);
    }

    public void EnableOutline()
    {
        if (isActive) return;
        isActive = true;

        // Add outline material as a second pass
        rend.materials = new Material[] { baseMaterial, outlineMaterial };
    }

    public void DisableOutline()
    {
        if (!isActive) return;
        isActive = false;

        // Remove outline, leave only base material
        rend.materials = new Material[] { baseMaterial };
    }

    public void ToggleOutline()
    {
        if (isActive) DisableOutline();
        else EnableOutline();
    }
}

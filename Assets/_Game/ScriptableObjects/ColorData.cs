using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData", order = 1)]
public class ColorData : ScriptableObject
{
    [SerializeField] Material[] materials;

    public Material GetColorMaterial(ColorType color)
    {
        return materials[(int)color];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : GameUnit
{
    public ColorType color;
    [SerializeField] Renderer mesh;
    [SerializeField] ColorData colorData;
    public void ChangeColor(ColorType color)
    {
        this.color = color;
        mesh.material = colorData.GetColorMaterial(color);
    }

    public override void OnDespawn()
    {
        
    }

    public override void OnInit()
    {
        
    }
}

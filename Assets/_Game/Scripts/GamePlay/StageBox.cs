using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBox : MonoBehaviour
{
    public Stage stage;
    List<ColorType> colors = new List<ColorType>();
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null && !colors.Contains(character.color))
        {
            colors.Add(character.color);
            character.stage = stage;
            stage.InitColor(character.color);            
        }
    }
}

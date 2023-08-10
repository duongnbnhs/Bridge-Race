using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lose : UICanvas
{
    public TextMeshProUGUI textMeshPro;
    public void RePlayBtn()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.lvIndex + 1);
        LevelManager.Instance.OnInit();
        UIManager.Instance.CloseUI<Lose>();
    }
}

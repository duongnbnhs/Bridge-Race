using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : UICanvas
{
    public TextMeshProUGUI textMeshPro;
    public void PlayBtn()
    {
        UIManager.Instance.OpenUI<GamePlay>();
        LevelManager.Instance.OnStartGame();
        Close();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Win : UICanvas
{
    public TextMeshProUGUI textMeshPro;
    public void RePlayBtn()
    {
        LevelManager.Instance.OnReset();
        LevelManager.Instance.LoadLevel(LevelManager.Instance.lvIndex);
        LevelManager.Instance.OnInit();
        UIManager.Instance.OpenUI<GamePlay>().textMeshPro.text = "Level " + (LevelManager.Instance.lvIndex + 1);
        LevelManager.Instance.OnStartGame();
        UIManager.Instance.CloseUI<Win>();
    }
    public void NextLvBtn()
    {
        LevelManager.Instance.OnReset();
        LevelManager.Instance.LoadLevel(LevelManager.Instance.lvIndex + 1);
        LevelManager.Instance.OnInit();
        UIManager.Instance.OpenUI<GamePlay>().textMeshPro.text = "Level " + (LevelManager.Instance.lvIndex + 1);
        LevelManager.Instance.OnStartGame();
        UIManager.Instance.CloseUI<Win>();
    }
}

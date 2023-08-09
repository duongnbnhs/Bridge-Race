using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBox : MonoBehaviour
{
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    private void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<Character>();
        if (character != null)
        {
            if (character.GetComponent<Player>())
            {
                GameManager.Instance.ChangeState(GameState.Finish);
                LevelManager.Instance.OnFinishGame();
                var win = UIManager.Instance.OpenUI<Win>();
                win.textMeshPro.text = "Level " + (LevelManager.Instance.lvIndex + 1);
                Debug.LogError("Player");
                SoundManager.Instance.PlaySound(winSound);
            }
            if (character.GetComponent<BOT>())
            {
                GameManager.Instance.ChangeState(GameState.Finish);
                LevelManager.Instance.OnFinishGame();
                var lose = UIManager.Instance.OpenUI<Lose>();
                lose.textMeshPro.text = "Level " + (LevelManager.Instance.lvIndex + 1);
                Debug.LogError("Bot");
                SoundManager.Instance.PlaySound(loseSound);
            }
            character.ChangeAnim("Dance");
            character.transform.eulerAngles = Vector3.up * 180;
            character.OnInit();
        }
    }
}

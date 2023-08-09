using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes = new List<ColorType>() { ColorType.Blue, ColorType.Yellow, ColorType.Orange, ColorType.Red, ColorType.Green, ColorType.Pink, ColorType.Cyan };
    public Level[] levelPrefabs;
    public Level currentLevel;
    public BOT botprefab;
    public Player player;
    public int lvIndex;
    List<BOT> bots = new();
    public int amountCharacter => currentLevel.amountBot + 1;
    public Vector3 finishPoint => currentLevel.finishPoint.position;
    private void Start()
    {
        lvIndex = 0;
        LoadLevel(lvIndex);
        OnInit();
    }
    public void OnInit()
    {
        Vector3 cen = currentLevel.startPoint.position;
        float space = 2f;
        Vector3 leftPoint = ((amountCharacter / 2) - (amountCharacter % 2) * 0.5f - 0.5f) * space * Vector3.left + cen;
        List<Vector3> startPoints = new List<Vector3>();

        for (int i = 0; i < amountCharacter; i++)
        {
            startPoints.Add(leftPoint + space * Vector3.right * i);
        }

        List<ColorType> colorDatas = Utilities.SortOrder(colorTypes, amountCharacter);

        int rand = Random.Range(0, amountCharacter);
        player.transform.position = startPoints[rand];
        player.transform.rotation = Quaternion.identity;
        startPoints.RemoveAt(rand);
        player.OnInit();
        player.ChangeAnim("Idle");
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);

        for (int i = 0; i < amountCharacter - 1; i++)
        {
            //BOT bot = Instantiate(botprefab, startPoints[i], Quaternion.identity);
            BOT bot = SimplePool.Spawn<BOT>(PoolType.Bot, startPoints[i], Quaternion.identity);
            bot.ChangeColor(colorDatas[i]);
            bots.Add(bot);
            bot.OnInit();
            bot.ChangeAnim("Idle");
        }
    }
    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            //NavmeshManager.Instance.UnBaker();
            Destroy(currentLevel.gameObject);
        }
        if (level < levelPrefabs.Length)
        {
            lvIndex = level;
            currentLevel = Instantiate(levelPrefabs[level]);
            currentLevel.OnInit();
        }
        else
        {
            level = levelPrefabs.Length - 1;
            lvIndex = level;
            currentLevel = Instantiate(levelPrefabs[level]);
            currentLevel.OnInit();
        }
        //NavmeshManager.Instance.Baker();
    }
    public void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.Play);
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new FindBrickState());
        }
    }
    public void OnFinishGame()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(null);
            bots[i].StopMoving();
        }
        UIManager.Instance.CloseUI<GamePlay>();
    }
    public void OnReset()
    {
        /*for (int i = 0; i < bots.Count; i++)
        {
            Destroy(bots[i].gameObject);
        }*/
        SimplePool.CollectAll();
        bots.Clear();
    }
}

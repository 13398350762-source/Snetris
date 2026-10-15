using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Random = UnityEngine.Random;

public enum Direction
{
    Down,
    Left,
    Right
}

public enum GameState
{
    Spawning,
    Moving,
    Resolving,
    Gameover
}
public class GameManager:MonoBehaviour
{
    static private GameManager instance;
    static public GameManager Instance => instance;

    private BoardData boardData=new BoardData();
    private SnakeData snakeData=new SnakeData();
    private GameState gameState = GameState.Spawning;
    private int score=0;
    private float internalCost=0.25f;
    public bool isPause;
    private bool isGameOver = false;
    private string nowTime = "";
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!UIManager.Instance.GetPanel<GameOverPanel>())
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
                snakeData.direction = Direction.Down;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                snakeData.direction = Direction.Right;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                snakeData.direction = Direction.Left;
            if (Input.GetKeyDown(KeyCode.Space))
                UIManager.Instance.GetPanel<GamePanel>().togStop.isOn = !UIManager.Instance.GetPanel<GamePanel>().togStop.isOn;
        }
    }
    public void StartGame()
    {
        isGameOver = false;
        gameState = GameState.Spawning;
        score = 0;
        UIManager.Instance.GetPanel<GamePanel>().UpdateScore(0, 0);
        snakeData = new SnakeData();
        StartCoroutine(SnakeController());
    }
    public IEnumerator SnakeController()
    {
        nowTime= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
        while (!isGameOver)
        {
            switch (gameState)
            {
                case GameState.Spawning:
                    yield return CreateSnake();
                    break;
                case GameState.Moving:
                    if (!snakeData.Move(boardData))
                    {
                        snakeData.ResolveSnakeFall(boardData);
                        AudioManager.Instance.PlaySound("ArtRes/Audio/Gameplay/block_drop");
                        gameState = GameState.Resolving;
                    }
                    UIManager.Instance.GetPanel<GamePanel>().UpateBoard();
                    //停顿间隔时间
                    yield return new WaitForSeconds(internalCost);
                    break;
                case GameState.Resolving:
                    yield return Resolve();
                    //处理完后判断是否为gamover阶段
                    if (IsGameOver())
                        gameState = GameState.Gameover;
                    else
                    {
                        gameState = GameState.Spawning;
                        UIManager.Instance.GetPanel<GamePanel>().UpateBoard();
                        //停顿一些时间
                        yield return new WaitForSeconds(internalCost);
                    }
                    break;
                case GameState.Gameover:
                    //显示GameOverPanel,现在先测试用tippanel
                    GameOver();
                    break;
            }
        }
        yield return null;
    }

    public IEnumerator CreateSnake()
    {
        int length = Random.Range(3, 6);
        BlockColor color = (BlockColor)Random.Range(1, 5);
        snakeData.color = color;
        int pos=Random.Range(0, 2);
        Vector2Int startpos;
        if (pos == 1)
        {
            startpos = new Vector2Int(0, 0);
            snakeData.direction = Direction.Right;
        }
        else
        {
            startpos = new Vector2Int(0, 8);
            snakeData.direction = Direction.Left;
        }
        for (int i = 0; i < length; i++)
        {
            if (!snakeData.Move(boardData))
            {
                gameState = GameState.Gameover;
                yield break;
            }
            snakeData.positions.Add(startpos);
            boardData.board[startpos.x, startpos.y] = snakeData.color;
            //间隔等待时间
            UIManager.Instance.GetPanel<GamePanel>().UpateBoard();
            yield return new WaitForSeconds(internalCost);
        }
        gameState = GameState.Moving;
        yield return null;
    }

    public IEnumerator Resolve()
    {
        int combo = 0;
        int num = 0;
        int count;
        while ((count=boardData.BlockErase())!=0)
        {
            combo++;
            num += count;
            UIManager.Instance.GetPanel<GamePanel>().UpateBoard();
            yield return new WaitForSeconds(0.03f);
        }
        if (num == 9)
        {
            AudioManager.Instance.PlaySound("ArtRes/Audio/Gameplay/match_3");
        }
        else if(num>9)
        {
            AudioManager.Instance.PlaySound("ArtRes/Audio/Gameplay/match_4");
        }
        score += combo * 10 *num;
        UIManager.Instance.GetPanel<GamePanel>().UpdateScore(score,combo);
        snakeData = new SnakeData();
        yield return null;
    }

    public bool IsGameOver()
    {
        for (int i = 0; i < BoardData.Col; i++)
        {
            if (boardData.board[0, i] != BlockColor.Null)
                return true;
        }
        return false;
    }

    public BoardData GetBoardData()
    {
        return boardData;
    }
    public void ChangeDirection(Direction dir)
    {
        snakeData.direction = dir;
    }

    private void GameOver()
    {
        isGameOver = true;
        AudioManager.Instance.PlaySound("ArtRes/Audio/Results/game_over");
        RankItem item = new RankItem();
        item.username = DataManger.Instance.username;
        item.record = score;
        item.date = nowTime;
        DataManger.Instance.AddRankItem(item);
        boardData.Clear();
        UIManager.Instance.GetPanel<GamePanel>().UpateBoard();
        UIManager.Instance.GetPanel<GamePanel>().txtRecord.text = DataManger.Instance.GetBestRecord().ToString();
        UIManager.Instance.ShowPanel<GameOverPanel>().txtScore.text = score.ToString();
    }

}

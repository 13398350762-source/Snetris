using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockColor
{
    Null,
    Coral,
    Cyan,
    Gold,
    Mint  
}

public class BoardData
{
    public const int Row=18;
    public const int Col = 9;
    public BlockColor[,] board=new BlockColor[Row, Col];
    private bool[,] eraseMask=new bool[Row, Col];
    private int[,] visited=new int[Row, Col];
    private List<HashSet<Vector2Int>> groups=new List<HashSet<Vector2Int>>();
    private List<Dictionary<Vector2Int,BlockColor>> colors=new List<Dictionary<Vector2Int, BlockColor>>();

    public int BlockErase()
    {
        int eraseBlock = 0;
        Array.Clear(eraseMask, 0, eraseMask.Length);
        bool isHaveEraseMask = false;
        for (int i = Row - 1; i >= 2; i--)
        {
            for (int j = 0; j < Col-2; j++)
            {
                if (board[i, j] == BlockColor.Null)
                    continue;
                else
                {
                    BlockColor color= board[i, j];
                    bool isErase = true;
                    for (int k = 0; k < 9; k++)
                    {
                        int row = i - k / 3;
                        int col= j + k % 3;
                        if (board[row, col] != color)
                        {
                            isErase = false;
                            break;
                        }
                    }
                    if (isErase)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            int row = i - k / 3;
                            int col = j + k % 3;
                            eraseMask[row, col] = true;
                        }
                        isHaveEraseMask = true;
                    }
                }
            }
        }
        if (!isHaveEraseMask)
            return 0;
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Col; j++)
            {
                if (eraseMask[i, j] == true)
                {
                    board[i, j] = BlockColor.Null;
                    eraseBlock++;
                }
            }
        }
        //处理下落
        while (ResolveFall())
        {
            colors.Clear();
            groups.Clear();
            Array.Clear(visited, 0, visited.Length);
        }
        colors.Clear();
        groups.Clear();
        Array.Clear(visited, 0, visited.Length);
        return eraseBlock;
    }

    private bool ResolveFall()
    {
        GetGroup();
        bool isLoop = false;
        for (int i = 0; i < groups.Count; i++)
        {
            bool isHaveFall = true;
            foreach (Vector2Int pos in groups[i])
            {
                if (pos.x + 1 >= Row || (board[pos.x + 1, pos.y] != BlockColor.Null && !groups[i].Contains(new Vector2Int(pos.x + 1, pos.y))))
                    isHaveFall = false;
            }
            if (isHaveFall)
            {
                isLoop = true;
                foreach (Vector2Int pos in groups[i])
                {
                    board[pos.x, pos.y] = BlockColor.Null;
                }
                foreach (Vector2Int pos in groups[i])
                {
                    board[pos.x + 1, pos.y] = colors[i][new Vector2Int(pos.x, pos.y)];
                }
                break;
            }
        }
        return isLoop;
    }

    static public string GetColorRes(BlockColor color)
    {
        string res = "";
        switch (color)
        {
            case BlockColor.Mint:res = "block_mint";
                break;
            case BlockColor.Coral:res = "block_coral";
                break;
            case BlockColor.Gold:res = "block_gold";
                break;
            case BlockColor.Cyan:res = "block_cyan";
                break;
        }
        return res;
    }

    private void GetGroup()
    {
        int num = 1;
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Col; j++)
            {
                if (board[i, j] == BlockColor.Null || visited[i,j]!=0)
                    continue;
                visited[i, j] = num;
                HashSet<Vector2Int> hash=new HashSet<Vector2Int>();
                Dictionary<Vector2Int,BlockColor> colorDic=new Dictionary<Vector2Int,BlockColor>();
                hash.Add(new Vector2Int(i, j));
                colorDic.Add(new Vector2Int(i, j), board[i, j]);
                BFSGroup(num, i + 1, j,hash, colorDic);
                BFSGroup(num, i - 1, j,hash, colorDic);
                BFSGroup(num, i, j + 1,hash, colorDic);
                BFSGroup(num, i, j - 1,hash, colorDic);
                num++;
                groups.Add(hash);
                colors.Add(colorDic);
            }
        }
    }

    private void BFSGroup(int num,int i,int j,HashSet<Vector2Int> hash, Dictionary<Vector2Int, BlockColor> colorDic)
    {
        if (i < 0 || i >= Row || j < 0 || j >= Col || visited[i, j] != 0 || board[i, j] == BlockColor.Null)
            return;
        visited[i, j] = num;
        hash.Add(new Vector2Int(i, j));
        colorDic.Add(new Vector2Int(i,j),board[i, j]);
        BFSGroup(num, i + 1, j, hash, colorDic);
        BFSGroup(num, i - 1, j, hash, colorDic);
        BFSGroup(num, i, j + 1, hash, colorDic);
        BFSGroup(num, i, j - 1, hash, colorDic);
    }

    public void Clear()
    {
        eraseMask = new bool[Row, Col];
        board = new BlockColor[Row, Col];
        visited = new int[Row, Col];
    }
}

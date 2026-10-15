using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeData
{
    public BlockColor color;
    public Direction direction;
    public List<Vector2Int> positions=new List<Vector2Int>();

    public bool Move(BoardData data)
    {
        if (positions.Count == 0)
            return true;
        Vector2Int newPos=positions[0];
        switch (direction)
        {
            case Direction.Left:newPos += new Vector2Int(0, -1);
                break;
            case Direction.Right:newPos+=new Vector2Int(0, 1);
                break;
            case Direction.Down:newPos += new Vector2Int(1, 0);
                break;
        }
        if (newPos.x>=18||newPos.y>=9||newPos.y<0||data.board[newPos.x, newPos.y] != BlockColor.Null)
            return false;
        Vector2Int oldPos = positions[positions.Count - 1];
        data.board[oldPos.x, oldPos.y] = BlockColor.Null;
        data.board[newPos.x, newPos.y] = color;
        for(int i=positions.Count-1;i>0;i--)
            positions[i]= positions[i-1];
        positions[0] = newPos;
        return true;
    }

    public void ResolveSnakeFall(BoardData data)
    {
        if (direction == Direction.Down)
            return;
        bool isFall = true;
        while (true)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i].x + 1 < BoardData.Row && (data.board[positions[i].x+1, positions[i].y] == BlockColor.Null || isSnakeBody(new Vector2Int(positions[i].x + 1, positions[i].y))))
                    continue;
                else
                {
                    isFall = false;
                    break;
                }
            }
            if (!isFall)
                break;
            for (int i = 0; i < positions.Count; i++)
            {
                data.board[positions[i].x + 1, positions[i].y]=color;
                data.board[positions[i].x, positions[i].y] = BlockColor.Null;
                positions[i]=new Vector2Int(positions[i].x + 1, positions[i].y);
            }
        }
    }

    private bool isSnakeBody(Vector2Int pos)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i] == pos)
                return true;
        }
        return false;
    }
}

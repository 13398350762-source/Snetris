using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CreateCells
{
    [MenuItem("Tools/Create Tetris Cells")]
    public static void Create()
    {
        GameObject parent = GameObject.Find("ImgGameBK");

        if (parent == null)
        {
            Debug.LogWarning("请先选中棋盘容器 Board");
            return;
        }

        // 支持撤销
        Undo.SetCurrentGroupName("Create Tetris Cells");

        for (int i = 0; i < 162; i++)
        {
            GameObject cell = GameObject.Instantiate(Resources.Load<GameObject>("Game/ImgItem"));

            Undo.RegisterCreatedObjectUndo(cell, "Create Cell");

            cell.transform.SetParent(parent.transform, false);

        }

        Debug.Log("成功创建162个格子");
    }
}

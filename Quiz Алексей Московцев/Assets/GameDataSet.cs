using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Data Set", menuName = "Game Data Set", order = 10)]
[Serializable]
public class GameDataSet : ScriptableObject
{
    [SerializeField]
    private int[] _rowNumber;

    [SerializeField]
    private int[] _columnNumber;

    [SerializeField]
    private Sprite[] _sprites;

    public int[] RowNumber => _rowNumber;
    public int[] ColumnNumber => _columnNumber;
    public Sprite[] Sprites => _sprites;

}

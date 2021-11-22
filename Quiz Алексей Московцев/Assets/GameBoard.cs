using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private GameDataSet[] dataSets;
    private int dataSetIndex;
    [SerializeField]
    private Cell cell;
    [SerializeField]
    private UnityEngine.UI.Text goalText;
    private string winSpriteName;
    public string WinSpriteName => winSpriteName;
    [SerializeField]
    private UnityEvent winEvent;
    public UnityEvent WinEvent => winEvent;
    private int gameRound = 0;
    private string[] usedGoals = new string[100];
    private int indexUsedGoals = 0;
    [SerializeField]
    private GameObject restartButton;
    [SerializeField]
    private Camera gameCamera;
    // Start is called before the first frame update
    private void Start()
    {
        NewRound();
        goalText.DOColor(Color.black, 5);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Game() //переход либо к генерации нового уровня, либо, если игра кончилась, к кнопке рестарта
    {
        gameRound++;
        if (gameRound < dataSets[dataSetIndex].ColumnNumber.Length)
        {
            NewRound();
        }
        else
        {
            goalText.text = "Restart?";
            Instantiate(restartButton, transform.position, transform.rotation).transform.SetParent(transform);
            gameCamera.DOColor(Color.black, 5);
        }
    }

    private void GenerateGameField(int columns, int rows, Sprite [] sprites) //генератор игрового поля
    {
        sprites = SpritesShuffle(sprites);
        int winIndex = 0;
        //Выбираем победный символ, по возможности предотвращаем повторение значений в рамках запущенной игры
        var hash = new HashSet<string>(usedGoals);
        int iterations = 0;
        while (iterations < 100)
        {
            winIndex = UnityEngine.Random.Range(0, columns * rows);
            if (!hash.Contains(sprites[winIndex].name))
            {
                break;
            }
            iterations++;
        }
        for (int i = 0; i < rows; i++) //заполнение игрового поля ячейками
        {
            for (int j = 0; j < columns; j++)
            {
                if (winIndex == i * columns + j)
                {
                    winSpriteName = sprites[i * columns + j].name;
                    usedGoals[indexUsedGoals] = winSpriteName;
                    indexUsedGoals++;
                    goalText.text = "Find " + winSpriteName;
                }
                cell.GetComponent<SpriteRenderer>().sprite = sprites[i * columns + j];
                Cell tempCell = Instantiate(cell, transform.position + new Vector3(7 * j, -7 * i), transform.rotation);
                tempCell.transform.SetParent(transform);
                winEvent.AddListener(tempCell.GetComponent<Cell>().Win);
                winEvent.AddListener(tempCell.GetComponentInChildren<Frame>().Win);
            }
        }
    }
    private Sprite[] SpritesShuffle(Sprite [] sprites) //перемешивание массива спрайтов (для вывода на экран случайного набора ячеек)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            Sprite currentSprite = sprites[i];
            int randomIndex = UnityEngine.Random.Range(0, sprites.Length);
            sprites[i] = sprites[randomIndex];
            sprites[randomIndex] = currentSprite;
        }
        return sprites;
    }
    public void Win()
    {
        Invoke("Game", 3);
    }
    public void Restart()
    {
        gameCamera.DOColor(Color.white, 5);
        gameRound = 0;
        NewRound();
    }
    private void NewRound() //вызов генератора игрового поля
    {
        dataSetIndex = UnityEngine.Random.Range(0, dataSets.Length);
        GenerateGameField(dataSets[dataSetIndex].ColumnNumber[gameRound], dataSets[dataSetIndex].RowNumber[gameRound], dataSets[dataSetIndex].Sprites);
    }
}

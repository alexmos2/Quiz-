using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    private GameBoard board;
    private string spriteName;
    private float shakeTime = 0;
    private bool isWinCell;
    private bool win = false;

    // Start is called before the first frame update
    private void Start()
    {
        board = GetComponentInParent<GameBoard>();
        spriteName = GetComponent<SpriteRenderer>().sprite.name;
        isWinCell = spriteName == board.WinSpriteName; //условие победы
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void OnMouseUpAsButton() //реакция на клик по спрайту мышью
    {
        if (!win)
        {
            if (isWinCell)
            {
                GetComponent<ParticleSystem>().Play();
                transform.DOShakePosition(2, 0.5f);
                board.WinEvent.Invoke();
            }
            else
            {
                if (Time.time >= shakeTime)
                {
                    shakeTime = Time.time + 2;
                    transform.DOShakePosition(2, 0.5f);
                }
            }
        } 
    }
    public void Win() //запланированное уничтожение ячеек при "победе"
    {
        win = true;
        Destroy(gameObject, 3);
    }
}

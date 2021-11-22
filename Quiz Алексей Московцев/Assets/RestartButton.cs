using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestartButton : MonoBehaviour
{
    [SerializeField]
    private UnityEvent restartEvent;
    // Start is called before the first frame update
    private void Start()
    {
        restartEvent.AddListener(GetComponentInParent<GameBoard>().Restart);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void OnMouseUpAsButton() //реакция на клик по спрайту мышью
    {
        restartEvent.Invoke();
        Destroy(gameObject);
    }
}

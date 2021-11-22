using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        transform.parent = null; //чтобы рамка не дергалась одновременно со спрайтом
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    public void Win()
    {
        Destroy(gameObject, 3); //запланированное разрушение всех рамок при правильном тапе
    }
}

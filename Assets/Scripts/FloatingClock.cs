using UnityEngine;
using System.Collections.Generic;

public class FloatingClock : FloatingGift
{
    public float bonusTime = 3f; 
    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
        initialPhase = Random.Range(0, 2 * Mathf.PI); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.instance.levelTimer += bonusTime;
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class FloatingGift : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f; 
    public float speed = 2f; 
    public float disappearHeight = 5f; 

    protected Vector3 startPosition;
    protected float startTime;
    protected float initialPhase;

    void Update()
    {

        float x = startPosition.x + amplitude * Mathf.Sin((Time.time - startTime) * frequency + initialPhase);
        float y = startPosition.y + speed * (Time.time - startTime);

        transform.position = new Vector3(x, y, startPosition.z);

        if (transform.position.y >= startPosition.y + disappearHeight)
        {
            Destroy(gameObject);
        }
    }

}

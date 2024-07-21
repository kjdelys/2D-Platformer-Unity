using System.Collections;
using UnityEngine;

public class RabbitAI : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float directionChangeInterval = 0.1f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        Move();
        Animate();
    }

    private void Move()
    {
        rb.velocity = movement * moveSpeed;
        isMoving = (Mathf.Abs(movement.x) > 0);
    }

    private void Animate()
    {
        animator.SetBool("isMovingRight", movement.x > 0);
        animator.SetBool("isMovingLeft", movement.x < 0);
        animator.SetBool("isIdle", !isMoving);
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            movement = new Vector2(Random.Range(-1f, 1f), 0);
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.instance.IncrementRabbitCount();

            Destroy(this.gameObject,0.2f);
            
        }
        
    }
}

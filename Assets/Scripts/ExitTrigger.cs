using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    //public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameManager.instance.WordCompleted)
            {
                StartCoroutine("LevelExit");
                collision.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator LevelExit()
    {
        //anim.SetTrigger("Exit");
        yield return new WaitForSeconds(0.1f);

        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(1f);
        // Do something after flag anim
        GameManager.instance.LevelComplete();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;

    private float min_x = -2.48f;
    private float max_x = 2.48f;

    public Text Score_Text;
    private int score;

    public float speed = 3f;

    public Button Restart;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(CountScore());
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlayerBound();
    }

    void PlayerBound()
    {
        Vector3 temp = transform.position;

        if(temp.x < min_x)
        {
            temp.x = min_x; 
        }
        else if(temp.x > max_x)
        {
            temp.x = max_x;
        }
        transform.position = temp;
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 temp = transform.position;
        
        if(h>0)
        {
            temp.x = temp.x + (speed * Time.deltaTime);
            sr.flipX = false;
            anim.SetBool("Walk", true);
        }
        else if(h<0)
        {
            temp.x = temp.x - (speed * Time.deltaTime);
            sr.flipX = true;
            anim.SetBool("Walk", true);
        }
        else 
        {
            anim.SetBool("Walk", false);
        }
        transform.position = temp;
    }

    /*IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }*/

    IEnumerator CountScore()
    {
        yield return new WaitForSeconds(1f);
        
        score++;

        Score_Text.text = "" + score;

        StartCoroutine(CountScore());
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Knife")
        {
            Time.timeScale = 0f;
            Restart.gameObject.SetActive(true);
        }
    }
}






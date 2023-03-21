using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public TextMeshProUGUI score;
    public TextMeshProUGUI liveCount;

    private int scoreValue = 0;
    private int liveValue = 3;

    public GameObject winTextObject;
    public GameObject loseTextObject;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    int level2 = 0;
    int win = 0;
    
    private bool facingRight = true;

    Animator anim;

    void Start()
    {
      rd2d = GetComponent<Rigidbody2D>();   
      score.text = scoreValue.ToString();
      liveCount.text = "Lives: " + liveValue.ToString();

      anim = GetComponent<Animator>();

      winTextObject.SetActive(false);
      loseTextObject.SetActive(false);
    }

    void FixedUpdate()
    {
     float hozMovement = Input.GetAxis("Horizontal");
     float vertMovement = Input.GetAxis("Vertical");
     rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed)); 

     if(scoreValue == 4)
     {
       if (level2 == 0)
       {
        transform.position = new Vector2(90.0f, 0f);
        scoreValue = 0;
        score.text = scoreValue.ToString();
        liveValue = 3;
        liveCount.text = "Lives: " + liveValue.ToString();
        level2 = 1;
       }
       else
       {
        winTextObject.SetActive(true);
        gameObject.CompareTag("Player");
        Destroy(gameObject);
        win = 1;
       }
     }

    if(liveValue <= 0)
     {
      loseTextObject.SetActive(true);
      gameObject.CompareTag("Player");
      Destroy(gameObject);
     }

    if(win == 1)
     {
      musicSource.clip = musicClipOne;
      musicSource.Play();
     }

    if (facingRight == false && hozMovement > 0)
   {
     Flip();
   }
    else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }

     if(Input.GetKeyDown(KeyCode.A))
     {
      anim.SetInteger("State", 1);
     }

     if (Input.GetKeyUp(KeyCode.A)) 
       {
        anim.SetInteger("State", 0);
        }
     if(Input.GetKeyDown(KeyCode.D))
     {
      anim.SetInteger("State", 1);
     }
     if (Input.GetKeyUp(KeyCode.D)) 
       {
        anim.SetInteger("State", 0);
        }
    }

  void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.collider.tag == "Coin")
      {
        scoreValue += 1;
        score.text = scoreValue.ToString();
        Destroy(collision.collider.gameObject);

      }

      if (collision.collider.tag == "Enemy")
      {
        liveValue = liveValue - 1;
        liveCount.text = "Lives: " + liveValue.ToString();
        Destroy(collision.collider.gameObject);
      }
    

    }


    void OnCollisionStay2D(Collision2D collision)
      {
        if(collision.collider.tag == "Ground")
        {
          if(Input.GetKey(KeyCode.W))
          {
            rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
          }
        }
      }
}

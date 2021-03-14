using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Text countText;
    public Text winText;
    public Text livesText;
    

    public float speed;

    private Rigidbody2D rd2d;
    private int count;
    private int lives;


    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        SetCountText();
        SetLivesText();
        winText.text = "";

      
    }

   
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            count = count + 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }

        else if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3),ForceMode2D.Impulse);
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
       if (count == 8)
        {
            winText.text = "You Win! Game by Jorge Benavides!";
        }

        if (count == 4)
        {
            transform.position = new Vector2(84.2f, 0f);
            lives = 3;
            livesText.text = "Lives:" + lives.ToString();
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives:" + lives.ToString();
        if (lives == 0)
        {
            winText.text = "Better Luck Next Time! Game by Jorge Benavides";
            DestroyScriptInstance();
        }

        void DestroyScriptInstance()
        {
            Destroy(this.gameObject);
        }
    }

}
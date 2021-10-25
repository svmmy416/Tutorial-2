using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text win;
    public Text lives;
    private int livesValue = 3;

    public Transform destination;
    private int marker = 1;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        win.text = "";
        lives.text = "Lives: " + livesValue.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    void Update()
    {
        if (scoreValue == 4 && marker == 1)
        {
            GameObject.FindWithTag("Player").transform.position = destination.transform.position;
            marker = marker + 1;
            livesValue = 3;
            lives.text = "Lives: " + livesValue.ToString();
        }
    }
    // Update is called once per frame
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
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue == 8)
        {
            win.text = "You Win! Game Created by Samuel Clarke";
             musicSource.clip = musicClipTwo;
             musicSource.Play();
             musicSource.loop = false;
            rd2d.isKinematic = true;
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (livesValue == 0)
        {
            win.text = "You Lose! Game Created by Samuel Clarke";
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
              rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse); 
            }
        }
    }
}

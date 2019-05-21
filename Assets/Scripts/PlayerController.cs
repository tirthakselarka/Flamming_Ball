using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    float speed = 8;

    public Text txtScore;
    int score;

    public ParticleSystem pSystemPlayer;
    public ParticleSystem pSystemEnemy;
    public GameObject panelGameOver;

    bool isGameOver;

    // Use this for initialization
    void Start() {

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update() {

        if (rb.velocity.magnitude < 2)
        {
            pSystemPlayer.Stop();
        }
        else {
            if (!pSystemPlayer.isPlaying)
            {
                pSystemPlayer.Play();
            }
            
        }

    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddForce(movement * speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            score++;
            txtScore.text = "Score : " + score;
        }

        if (other.gameObject.tag == "Enemy")
        {
            // Game Over
            isGameOver = true;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            pSystemEnemy.Play();
            Destroy(other.gameObject, 0.5f);
            panelGameOver.SetActive(true);
        }
    }

    public void playAgainUI()
    {
        SceneManager.LoadScene("GamePlay");
    }
}

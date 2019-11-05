using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float jumpForce; // 점프 높이

    private Rigidbody2D rd2; // 플레이어의 Rigidbody

    [SerializeField]
    private Text scoreText; // scoreText의 UI
    [SerializeField]
    private Text timeText; // timeText의 UI

    private float time; // 타이머 변수
    private float score; // 스코어 변수

    public bool gameOver = false;
    [SerializeField]
    private GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        time = 60;
        score = 0;
        rd2 = GetComponent<Rigidbody2D>();
        gameOver = false;
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            PlyerController();
            time -= Time.deltaTime;
            timeText.text = time.ToString("N0");
        }
        
        if (time <= 0)
        {
            gameOver = true;
            gameOverText.SetActive(true);
            time = 60;
        }
    }

    // 플레이어 이동 함수
    public void PlyerController()
    {
        if (Input.GetButtonDown("Fire1") && rd2.velocity.y == 0)
        {
            rd2.velocity = new Vector2(0.5f, jumpForce);

        }

        if(transform.position.x < -9 || transform.position.y < -6)
        {
            gameOver = true;
            gameOverText.SetActive(true);
        }
    }

    // 플레이어 충돌체크
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ruby")
        {
            score ++;
            scoreText.text = " " + score;
            collision.gameObject.SetActive(false);
        }
    }
    
    // Replay
    public void ReplayGame()
    {
        SceneManager.LoadScene("JumpGame");
    }

   
}

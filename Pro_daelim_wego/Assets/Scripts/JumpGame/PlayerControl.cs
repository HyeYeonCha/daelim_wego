using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float maxHeight; // 최대 점프 높이
    [SerializeField]
    private float minHeight; // 최소 높이

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
        if (transform.position.y < minHeight)
        {
            rd2.transform.position = new Vector2(-6f, minHeight);
        }
        if (Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)
        {
            rd2.velocity = new Vector2(0.0f, jumpForce);
        }
    }

    // 플레이어 충돌체크
       private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            gameOver = true;
            gameOverText.SetActive(true);
        }
        if (collision.gameObject.tag == "Pivot")
        {
            score += 100;
            scoreText.text = "Score : " + score;
        }
    }
}

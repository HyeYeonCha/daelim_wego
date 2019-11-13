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
    private BoxCollider2D bx2; // 플레이어의 Collider

    [SerializeField]
    private Text rubyScoreText; // rubyScoreText의 UI
    [SerializeField]
    private Text timeText; // timeText의 UI
    [SerializeField]
    private Text scoreText; // scoreText의 UI

    [SerializeField]
    private Text gameStartText; // 시작전 UI

    private float time; // 타이머 변수
    private float rubyScore; // 루비 스코어 변수
    private float score; // 스코어 변수

    static bool gameOver = false; // game over를 체크해주는 flag
    [SerializeField]
    private GameObject gameOverText; // game over일때 띄워주는 패널

    private AudioSource ruby_SFX; // 루비를 먹었을 때 효과음

    [SerializeField]
    private Text highScoreText; // highScoreText UI
    private float highScore; // high score를 담을 변수

    // Start is called before the first frame update
    void Start()
    {
        time = 60;
        rubyScore = 0;
        score = 0;

        rd2 = GetComponent<Rigidbody2D>();
        bx2 = GetComponent<BoxCollider2D>();
        ruby_SFX = GetComponent<AudioSource>();

        rd2.isKinematic = true;
        bx2.enabled = false;
        gameOver = true;
        gameOverText.SetActive(false);

        gameStartText.enabled = true;
        gameStartText.text = "Click !!!";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            time -= Time.deltaTime;
            timeText.text = time.ToString("N0");

            score += Time.deltaTime;
            scoreText.text = "Score : " + score.ToString("N0");

            PlyerController();

        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(GameStart());
            }
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
        rd2.isKinematic = false;
        bx2.enabled = true;

        if (Input.GetButtonDown("Fire1") && (rd2.velocity.y <= 0 && rd2.velocity.y >= -1)) // 가속도가 -1일때도 허용 
        {
            rd2.velocity = new Vector2(0.5f, jumpForce);
        }

        if(transform.position.x < -9 || transform.position.y < -6)
        {
            gameOver = true;
            gameOverText.SetActive(true);
        }
    }

    IEnumerator GameStart()
    {
        gameStartText.text = "Game Start !!!";
        rd2.velocity = new Vector2(0.5f, 2f);

        yield return new WaitForSeconds(1.5f);

        gameOver = false;
        gameStartText.enabled = false;
        
        PlyerController();
    }

    // 플레이어 충돌체크
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ruby")
        {
            rubyScore ++;
            rubyScoreText.text = " : " + rubyScore;
            collision.gameObject.SetActive(false);
            score += 100;
            scoreText.text = "Score : " + score;
            ruby_SFX.Play();
        }
    }
    
    // Replay
    public void ReplayGame()
    {
        SceneManager.LoadScene("JumpGame");
    }

   
}

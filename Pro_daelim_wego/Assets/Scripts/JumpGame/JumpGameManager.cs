using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player; // 플레이어 캐릭터

    [SerializeField]
    private float jumpForce; // 점프 속도
    [SerializeField]
    private float walkForce; // 이동 속도
    
    [SerializeField]
    private Rigidbody2D rd2; // 플레이어의 Rigidbody

    [SerializeField]
    private Text scoreText; // scoreText의 UI
    [SerializeField]
    private Text timeText; // timeText의 UI

    private float time; // 타이머 변수
    private float score; // 스코어 변수

    private bool isGameOver; // 게임 종료 체크 변수

    // Start is called before the first frame update
    void Start()
    {
        time = 60;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
        {
            PlyerController();

            time -= Time.deltaTime;
            timeText.text = time.ToString("N0");
        }
        if (time <= 0)
        {
            isGameOver = true;
            time = 60;
        }
    }


    // 플레이어 이동 함수
    public void PlyerController()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Player.transform.Translate(Vector3.right * walkForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Player.transform.Translate(Vector3.left * walkForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if(rd2.velocity.y == 0)
            Player.transform.Translate(Vector3.up * jumpForce);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        score += 100;
        scoreText.text = "Score : " + score;
    }




}

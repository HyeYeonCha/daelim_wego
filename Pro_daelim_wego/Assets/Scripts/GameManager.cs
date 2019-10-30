using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText; // 게임점수 반환
    private int score = 0; // 게임점수

    [SerializeField]
    private Text timeText; // 제한시간 반환
    private float time = 60.0f; // 제한시간

    private int randomLevel; // 출력될 화살표의 클론수
    private bool gameOver; // 게임오버(타임오버) 체크
    private bool removeFlag; // 매 라운드 게임오버 체크

    public List<GameObject> CheckBlock = new List<GameObject>(); // 랜덤하게 생성될 ArrowClones들의 값 >> 태그로 저장
    public List<string> PlayerBlock = new List<string>(); // 플레이어의 입력값 저장 

    [SerializeField]
    private GameObject[] ArrowBlock; // 랜덤하게 생성할 Arrow들의 부모객체
    private int randomIndex; // 부모 Arrow들을 랜덤하게 뽑을 인덱스

    [SerializeField]
    private GameObject Arrowclones; // 하이어라키뷰에 clones들을 정리하기위한 부모오브젝트

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        removeFlag = false;

        randomIndex = Random.Range(0, 3);
        SetBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            PlayerCheck();

            time -= Time.deltaTime;

            //timeText.text = "" + Mathf.Round(time);
            timeText.text = time.ToString("N0");
        }


        if (time <= 0)
        {
            gameOver = true;
            time = 60;
        }
    }

    // 플레이어의 입력값 ArrayList와 랜덤으로 생성된 ArrowClone ArrayList의 각 인덱스 값 비교 후 점수 반환
    public void CheckArrow()
    {
        if (!removeFlag)
        {
            for (int i = 0; i < PlayerBlock.Count; i++)
            {
                if (CheckBlock[i].tag == PlayerBlock[i])
                {
                    Debug.Log("score ++ : " + score);
                    score += 100;
                    scoreText.text = "Score : " + score;
                    removeFlag = false;
                }
                else
                {
                    Debug.Log("score -- : " + score);
                    score -= 100;
                    scoreText.text = "Score : " + score;
                    removeFlag = true;
                    DestroyBlock();
                    return;
                }
            }

        }

    }

    public void SetBlock()
    {
        randomLevel = Random.Range(3, 6);

        for (int i = 0; i < randomLevel + 1; i++)
        {
            randomIndex = Random.Range(0, 3);
            CheckBlock.Add(Instantiate(ArrowBlock[randomIndex], new Vector3(100 + (i * 80), 270, 0), Quaternion.identity) as GameObject);
            CheckBlock[i].transform.SetParent(Arrowclones.transform);
            Debug.Log(" 생성");
        }
    }

    public void DestroyBlock()
    {
        for (int i = 0; i < randomLevel + 1; i++)
        {
            Destroy(CheckBlock[i]);
        }
        CheckBlock.Clear();
        PlayerBlock.Clear();
        SetBlock();
        removeFlag = false;
    }

    // 플레이어의 입력값 반환하여 ArrayList에 저장
    public void PlayerCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerBlock.Add("LEFT");
            CheckArrow();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerBlock.Add("RIGHT");
            CheckArrow();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerBlock.Add("UP");
            CheckArrow();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerBlock.Add("DOWN");
            CheckArrow();
        }
    }
}

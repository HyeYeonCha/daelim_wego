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
    private float time = 5.0f; // 제한시간

    private int randomLevel = 4; // 출력될 화살표의 클론수
    private bool gameOver = false; // 게임오버 체크
    private bool checkFlag = true; // 매 라운드 체크

    private ArrayList CheckBlock = new ArrayList(); // 랜덤하게 생성될 ArrowClones들의 값 >> 태그로 저장
    private ArrayList PlayrBlock = new ArrayList(); // 플레이어의 입력값 저장 

    [SerializeField]
    private GameObject[] ArrowBlock; // 랜덤하게 생성할 Arrow들의 부모객체
    private int randomIndex; // 부모 Arrow들을 랜덤하게 뽑을 인덱스

    [SerializeField]
    private GameObject Arrowclones; // 하이어라키뷰에 clones들을 정리하기위한 부모오브젝트

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (checkFlag)
            {
                randomLevel = Random.Range(3, 6);
                randomIndex = Random.Range(0, 3);
                SetBlock();
                CheckArrow();
            }
           
            time -= Time.deltaTime;

            //timeText.text = "" + Mathf.Round(time);
            timeText.text = time.ToString("N0");
        }

        if(time <= 0)
        {
            gameOver = true;
            time = 60;
        }
    }

    // 플레이어의 입력값 ArrayList와 랜덤으로 생성된 ArrowClone ArrayList의 각 인덱스 값 비교 후 점수 반환
    public void CheckArrow()
    {
        
        //if()
        //{
        //    score += 100;
        //    scoreText.text = "Score : " + score;
        //}
        //else
        //{
        //    score -= 100;
        //    scoreText.text = "Score : " + score;
        //}
    }

    // 랜덤 인덱스를 이용한 클론 화살표 생성 (메뉴바에 생성)
    public void SetBlock()
    {
        for (int i=0; i < randomLevel+1; i++)
        {
            GameObject cloneArrow = Instantiate(ArrowBlock[randomIndex], new Vector3(100 + (i * 80), 270, 0), Quaternion.identity);
            CheckBlock.Add(cloneArrow.tag);
            cloneArrow.transform.parent = Arrowclones.transform;
            randomIndex = Random.Range(0, 3);
            // Debug.Log(CheckBlock[i] + "생성");
        }
        checkFlag = false;
    }

    // 플레이어의 입력값 반환하여 ArrayList에 저장
    public void PlayerCheck()
    {

    }

}

﻿using System.Collections;
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

    [SerializeField]
    private List<GameObject> CheckBlock = new List<GameObject>(); // 랜덤하게 생성될 ArrowClones들의 값 >> 태그로 저장
    [SerializeField]
    private List<string> PlayerBlock = new List<string>(); // 플레이어의 입력값 저장 
    [SerializeField]
    private List<GameObject> CanvasBugerBlock = new List<GameObject>(); // 랜덤하게 생성할 CnavasBugerClone들의 오브젝트들
    [SerializeField]
    private List<GameObject> BugerBlock = new List<GameObject>(); // 입력한 버거 값들 반환

    [SerializeField]
    private GameObject[] ArrowBlock; // 랜덤하게 생성할 Arrow들의 부모오브젝트
    [SerializeField]
    private GameObject[] CanvasP_Buger; // 랜덤하게 생성할 Canvas안의 Buger들 이미지의 부모
    [SerializeField]
    private GameObject[] BugerPObject; // 랜덤하게 생성할 Buger들의 부모오브젝트
    private int randomIndex; // 부모 Arrow들을 랜덤하게 뽑을 인덱스

    [SerializeField]
    private GameObject ArrowClones; // 하이어라키뷰에 ArrowClones들을 정리하기위한 부모오브젝트
    [SerializeField]
    private GameObject CanvasBugerClones; // 하이어라키뷰에 CanvasBugerClones들을 정리하기위한 부모오브젝트
    [SerializeField]
    private GameObject BugerObjectClones; // 하이어라키뷰에 Buger Clone Objects 들을 정리하기 위한 부모오브젝트

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
    public void ScoreCheck()
    {
        if (!removeFlag)
        {
            for (int i = 0; i < CheckBlock.Count; i++)
            {
                if(CheckBlock.Count >= PlayerBlock.Count)
                {
                    if (CheckBlock[i].tag == PlayerBlock[i])
                    {
                        Debug.Log("score ++ : " + score);
                        score += 100;
                        scoreText.text = "Score : " + score;
                        removeFlag = false;
                        // 여기서 오류남 >> 너무 많은 버거 생성과 랜덤인덱스가 들어가질 않음. >> 다른 함수에 있는거라 
                        BugerBlock.Add(Instantiate(BugerPObject[randomIndex], new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject);
                        BugerBlock[i].transform.SetParent(BugerObjectClones.transform);
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
                    
                } else if(CheckBlock.Count < PlayerBlock.Count) // 플레이어의 입력값이 주어진 값보다 많아질때
                {
                    DestroyBlock();
                }
                               
                
            }

        }

    }

    // 매 라운드마다 화살표 블럭과 버거블록을 새로 생성해줌.
    public void SetBlock()
    {
        randomLevel = Random.Range(3, 6);

        for (int i = 0; i < randomLevel + 1; i++)
        {
            randomIndex = Random.Range(0, 3);
            CheckBlock.Add(Instantiate(ArrowBlock[randomIndex], new Vector3(100 + (i * 80), 250, 0), Quaternion.identity) as GameObject);
            CheckBlock[i].transform.SetParent(ArrowClones.transform);
            CanvasBugerBlock.Add(Instantiate(CanvasP_Buger[randomIndex], new Vector3(100 + (i * 80), 280, 0), Quaternion.identity) as GameObject);
            CanvasBugerBlock[i].transform.SetParent(CanvasBugerClones.transform);
        }
    }

    // 매 라운드가 끝날때마다 생성된 Arrow GameObject Clones을 없애줌.
    public void DestroyBlock()
    {
        for (int i = 0; i < randomLevel + 1; i++)
        {
            Destroy(CheckBlock[i]);
            Destroy(CanvasBugerBlock[i]);
            Destroy(BugerBlock[i]);
        }
        CheckBlock.Clear();
        PlayerBlock.Clear();
        CanvasBugerBlock.Clear();
        BugerBlock.Clear();
        SetBlock();
        removeFlag = false;
    }

    // 플레이어의 입력값 반환하여 ArrayList에 저장
    public void PlayerCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerBlock.Add("LEFT");
            ScoreCheck();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerBlock.Add("RIGHT");
            ScoreCheck();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerBlock.Add("UP");
            ScoreCheck();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerBlock.Add("DOWN");
            ScoreCheck();
        }
        if(CheckBlock.Count == PlayerBlock.Count)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                DestroyBlock();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText; // 게임점수 반환
    private int score = 0; // 게임점수

    [SerializeField]
    private Text timeText; // 제한시간 반환
    private float time = 60.0f; // 제한시간

    private bool gameOver; // 게임오버(타임오버) 체크
    private bool removeFlag; // 매 라운드 게임오버 체크

    private int randomindex; // 매 라운드 레시피의 종류를 랜덤 생성할 인덱스
    private int randomLevel; // 매 라운드 레시피 개수 랜덤 생성할 인덱스

    [SerializeField]
    private List<GameObject> PlayerBuger = new List<GameObject>(); // 플레이어가 입력한 버거 재료들 저장 
    [SerializeField]
    private List<GameObject> CompleteBuger = new List<GameObject>(); // 완성된 버거 저장
    [SerializeField]
    private GameObject[] PBugerIng; // 버거 재료들의 부모객체

    private float x, y = 0; // 커서의 좌표

    [SerializeField]
    private GameObject[,] BugerIngredients = new GameObject[3,3]; // 버거 재료들의 고정 배열
    
    [SerializeField]
    private GameObject CompleteBugerClones; // 하이어라키뷰에 완성된 버거를 정리하기위한 부모오브젝트
    [SerializeField]
    private GameObject PlayerBugerClones; // 하이어라키뷰에 생성된 버거를 정리하기 위한 부모오브젝트

    [SerializeField]
    private GameObject Cursor;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        removeFlag = false;

        SetBuger();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                BugerIngredients[i, j] = PBugerIng[i];
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            PlayerCursor();
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

    // 플레이어가 생성한 버거 재료배열과 완성되어있던 버거 재료배열 비교후 점수 반환
    public void ScorePlus()
    {
        if (!removeFlag)
        {
            if (CompleteBuger.Last() == PlayerBuger.Last())
            {
                score += 100;
            }
            else
            {
                removeFlag = true;
                score -= 100;
                DestroyBuger();
            }

        }

    }

    // 매 라운드 랜덤한 버거 레시피 생성
    public void SetBuger()
    {
        randomLevel = Random.Range(3, 6);

        CompleteBuger.Add((Instantiate(PBugerIng[0], new Vector3(4, 3, 0), Quaternion.identity) as GameObject));
        CompleteBuger[0].transform.SetParent(CompleteBugerClones.transform);

        /* 11월 1일 정리 >> 게임 뒤엎었음 
         * 오브젝트로 커서 이동시 이동한 객체 출력하기 >> 박스콜라이더 수정해서
         * gameObject 삭제시키면 하나 남을것 같음 저거 버그 수정하기 (부모오브젝트로 하나가 안들어감)
         * 점수 체크는 마지막에 엔터키 입력시 가능하도록 수정.
         * 피버 만들어서 어느정도 점수가 오르고 게이지가 차면 엔터만으로도 버거 생성되도록 하는것 >> 이건 나중에 시간남으면...
         */
        for (int i = 0; i < randomLevel + 1; i++)
        {
            //StartCoroutine(DelayBuger());
            randomindex = Random.Range(2, 8);
            CompleteBuger.Add((Instantiate(PBugerIng[randomindex], new Vector3(4, 5, 0+ (i * -0.1f)), Quaternion.identity) as GameObject));
           // CompleteBuger[i]
            CompleteBuger[i].transform.SetParent(CompleteBugerClones.transform);
            
        }
        StartCoroutine(LastBugerIngredient());
    }

    IEnumerator LastBugerIngredient ()
    {
        yield return new WaitForSeconds(1.5f);
        CompleteBuger.Add((Instantiate(PBugerIng[1], new Vector3(4, 6, -2), Quaternion.identity) as GameObject));
        Debug.Log(CompleteBuger.Count + " + 랜덤 >>" + randomLevel);
        CompleteBuger[randomLevel + 2].transform.SetParent(CompleteBugerClones.transform);
    }
    IEnumerator DelayBuger()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log("Delay buger");
    }

    // 매 라운드가 끝날때마다 생성된 Arrow GameObject Clones을 없애줌.
    public void DestroyBuger()
    {
        for (int i = 0; i < randomLevel + 1; i++)
        {
            Destroy(CompleteBuger[i]);
            Destroy(PlayerBuger[i]);
        }
        CompleteBuger.Clear();
        PlayerBuger.Clear();
        SetBuger();
        removeFlag = false;
    }

    // 플레이어의 입력값 반환하여 ArrayList에 저장
    public void PlayerCursor()
    {
        x = Cursor.transform.position.x;
        y = Cursor.transform.position.y;

        if(Cursor.transform.position.x > 6)
        {
            Cursor.transform.position = new Vector3(6, y);
        }
        if (Cursor.transform.position.x < 2)
        {
            Cursor.transform.position = new Vector3(2, y);
        }
        if (Cursor.transform.position.y > -1)
        {
            Cursor.transform.position = new Vector3(x, -1.2f);
        }
        if (Cursor.transform.position.y < -2)
        {
            Cursor.transform.position = new Vector3(x, -2.8f);
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Cursor.transform.position = new Vector3(x - 2, y);
            Debug.Log(Cursor.transform.localPosition);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            Cursor.transform.position = new Vector3(x + 2, y);
            Debug.Log(Cursor.transform.localPosition);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            Cursor.transform.position = new Vector3(x, y + 0.8f);
            Debug.Log(Cursor.transform.localPosition);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            Cursor.transform.position = new Vector3(x, y - 0.8f);
            Debug.Log(Cursor.transform.localPosition);
        }
        if (CompleteBuger.Count == PlayerBuger.Count)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DestroyBuger();
                ScorePlus();
            }
        }

    }
}

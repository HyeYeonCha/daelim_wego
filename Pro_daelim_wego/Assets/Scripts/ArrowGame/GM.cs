using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GM : MonoBehaviour
{
    [SerializeField]
    private GameObject player; // 플레이어 캐릭터
    [SerializeField]
    private float playerSpeed; // 플레이어 스피드

    [SerializeField]
    private Text timeText; // 타이머 UI
    [SerializeField]
    private Text startText; // 게임시작을 알려주는 텍스트 UI

    private float time; // 타이머 text

    [SerializeField]
    private GameObject lightning; // 번개모양 장애물 
    [SerializeField]
    private GameObject lightningClones; // 장애물들을 하이어라키뷰에서 정리해줄 부모오브젝트

    private bool isGameOver = true;
    [SerializeField]
    private GameObject gameOverText;
    
    // Start is called before the first frame update
    void Start()
    {
        time = 60;
        gameOverText.SetActive(false);
        isGameOver = true;

        startText.text = "Click !!";

        if(Input.GetMouseButtonDown(0))
        {
            startText.text = "GameStart !!";
            StartCoroutine(GameStartText());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
        {
            PlayerController();
        } else
        {

        }
    }

    IEnumerator GameStartText ()
    {
        yield return new WaitForSeconds(3.0f);
        startText.enabled = false;
        isGameOver = false;
    }

    private void PlayerController()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        player.transform.position += new Vector3(horizontal * playerSpeed * Time.deltaTime, vertical * playerSpeed * Time.deltaTime);

    }


}

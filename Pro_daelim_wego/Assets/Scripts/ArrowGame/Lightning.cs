using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    private float speed;

    Vector3 playerPosition;

    GM gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gm.isGameOver)
        {
            MoveToPlayer();
        }
        
        if(3.5f <= Vector3.Distance(new Vector3(0, 0, 0), transform.position))
        {
            Destroy(gameObject);
        }
    }

    // 이동 수정하기 직선방면으로만 가도록
    private void MoveToPlayer()
    {
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * speed);
    }

}

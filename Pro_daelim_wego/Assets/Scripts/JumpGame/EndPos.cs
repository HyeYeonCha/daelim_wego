using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPos : MonoBehaviour
{
    [SerializeField]
    private GameObject Ruby1;
    [SerializeField]
    private GameObject Ruby2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Ruby")
        {
            Debug.Log("Ruby111");
            Ruby1.SetActive(true);
            Ruby2.SetActive(true);
            Debug.Log("Ruby22");
            collision.gameObject.SendMessage("ChangeLocation", SendMessageOptions.RequireReceiver);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ItemInfo : ScriptableObject
{
    [SerializeField]
    private string itemName; // 아이템 이름
    [SerializeField]
    private Sprite sprite; // 아이템 이미지
    [SerializeField]
    private int cost; // 아이템 가격
    [SerializeField]
    private string explain; // 아이템 설명
}

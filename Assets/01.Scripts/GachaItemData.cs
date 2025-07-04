using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewGachaItem", menuName = "Gacha/Item")]
public class GachaItemData : ScriptableObject
{
    public string itemName;
    public float probability; // 가중치 기반 확률
    public Sprite icon;       // UI 아이콘 이미지
    public string description;
}

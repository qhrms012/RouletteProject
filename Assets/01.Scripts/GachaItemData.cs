using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewGachaItem", menuName = "Gacha/Item")]
public class GachaItemData : ScriptableObject
{
    public string itemName;
    public float probability; // ����ġ ��� Ȯ��
    public Sprite icon;       // UI ������ �̹���
    public string description;
}

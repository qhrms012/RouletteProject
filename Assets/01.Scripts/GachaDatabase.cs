using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GachaDatabase", menuName = "Gacha/Database")]
public class GachaDatabase : ScriptableObject
{
    public GachaItemData[] items;
}


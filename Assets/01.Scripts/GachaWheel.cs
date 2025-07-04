using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GachaWheel : MonoBehaviour
{
    public Transform wheel;
    public Button spinButton;
    public GachaDatabase gachaDatabase;

    private bool isSpinning = false;

    void Start()
    {
        spinButton.onClick.AddListener(() =>
        {
            if (!isSpinning)
            {
                StartCoroutine(Spin());
            }
        });
    }

    IEnumerator Spin()
    {
        isSpinning = true;

        // 1. 아이템 선택
        GachaItemData selectedItem = GetRandomItem();
        int itemCount = gachaDatabase.items.Length;
        int index = System.Array.IndexOf(gachaDatabase.items, selectedItem);

        // 2. 각도 계산
        float segmentAngle = 360f / itemCount;
        float minAngle = index * segmentAngle;
        float maxAngle = minAngle + segmentAngle;
        float targetAngle = Random.Range(minAngle, maxAngle);

        // 3. 회전
        float startAngle = wheel.eulerAngles.z;
        float endAngle = 360f * Random.Range(5, 7) + targetAngle;
        float duration = 4f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float eased = Mathf.SmoothStep(0, 1, t);
            float angle = Mathf.Lerp(startAngle, endAngle, eased);
            wheel.eulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }

        Debug.Log($"획득 아이템: {selectedItem.itemName}");
        isSpinning = false;
    }

    GachaItemData GetRandomItem()
    {
        var items = gachaDatabase.items;
        float total = items.Sum(i => i.probability);
        float rand = Random.Range(0, total);
        float sum = 0;

        foreach (var item in items)
        {
            sum += item.probability;
            if (rand <= sum)
                return item;
        }

        return items.Last();
    }
}

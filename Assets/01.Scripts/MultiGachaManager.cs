using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MultiGachaManager : MonoBehaviour
{
    public Button multiDrawButton;
    public GameObject resultPanel;
    public Transform resultGrid;
    public GameObject resultSlotPrefab;

    public GachaDatabase gachaDatabase;

    void Start()
    {
        resultPanel.SetActive(false);
        multiDrawButton.onClick.AddListener(OnClickMultiDraw);
    }

    void OnClickMultiDraw()
    {
        List<GachaItemData> results = new List<GachaItemData>();
        for (int i = 0; i < 10; i++)
            results.Add(GetRandomItem());

        ShowResult(results);
    }

    void ShowResult(List<GachaItemData> results)
    {
        resultPanel.SetActive(true);

        // 기존 결과 삭제
        foreach (Transform child in resultGrid)
            Destroy(child.gameObject);

        foreach (var item in results)
        {
            var slot = Instantiate(resultSlotPrefab, resultGrid);
            slot.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
            slot.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.itemName;
            slot.transform.Find("Grade").GetComponent<TextMeshProUGUI>().text = $"{item.probability}%";
        }
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

        return items.Last(); // fallback
    }
}


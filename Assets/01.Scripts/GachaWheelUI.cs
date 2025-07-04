using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaWheelUI : MonoBehaviour
{
    public GachaDatabase gachaDatabase;
    public GameObject sectorPrefab; // ������ ���� Sector ������
    public Transform wheelParent;   // ���� UI ȸ���� �θ� ������Ʈ

    void Start()
    {
        CreateWheelSectors();
    }

    void CreateWheelSectors()
    {
        int itemCount = gachaDatabase.items.Length;
        float segmentAngle = 360f / itemCount;

        for (int i = 0; i < itemCount; i++)
        {
            var item = gachaDatabase.items[i];

            GameObject sector = Instantiate(sectorPrefab, wheelParent);

            RectTransform rt = sector.GetComponent<RectTransform>();
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0f);
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;

            
            float angle = -segmentAngle * i - (segmentAngle / 2f);
            rt.localRotation = Quaternion.Euler(0, 0, angle);

            // UI ����
            rt.Find("Icon").GetComponent<Image>().sprite = item.icon;
            rt.Find("NameText").GetComponent<TextMeshProUGUI>().text = item.itemName;
            rt.Find("ProbabilityText").GetComponent<TextMeshProUGUI>().text = $"{item.probability}%";
            rt.Find("DescriptionText").GetComponent<TextMeshProUGUI>().text = item.description;
        }
    }



}

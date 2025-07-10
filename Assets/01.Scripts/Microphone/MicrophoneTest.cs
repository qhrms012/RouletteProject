using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneTest : MonoBehaviour
{
    private AudioSource audioSource;
    public Image soundImage;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(Microphone.devices.Length > 0)
        {
            string mic = Microphone.devices[0];
            Debug.Log("������� ����ũ : " + mic);

            audioSource.clip = Microphone.Start(mic, true, 10, 44100);
            audioSource.loop = true;

            while (!(Microphone.GetPosition(mic) > 0)) {}

            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("����ũ ��ġ�� ã���� �����ϴ�");
        }
    }
    void Update()
    {
        float[] samples = new float[256];
        audioSource.GetOutputData(samples, 0);

        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }

        float rms = Mathf.Sqrt(sum / samples.Length);
        float db = 20 * Mathf.Log10(rms / 0.1f);

        // 1�ܰ�: ������ �Ӱ谪 ����
        float thresholdDb = -20f;
        db = Mathf.Max(db, thresholdDb);

        // 2�ܰ�: ����ȭ �� ���� ����
        float sensitivity = 1.0f;
        float normalizedVolume = Mathf.Clamp01(sensitivity * Mathf.InverseLerp(-20f, 0f, db));

        // 3�ܰ�: �ε巴�� �ð�ȭ
        float currentFill = soundImage.fillAmount;
        soundImage.fillAmount = Mathf.Lerp(currentFill, normalizedVolume, Time.deltaTime * 10f);



    }


}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneTest : MonoBehaviour
{
    private AudioSource audioSource;

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

    
}

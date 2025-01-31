using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public AudioClip[] musicClips; // ������ �����������
    private AudioSource audioSource;
    private int currentClipIndex;

    public float fadeDuration = 1.0f; // ����� ���������

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentClipIndex = 0;
        PlayMusic();
        InvokeRepeating("ChangeMusic", 20.0f, 10.0f); // ����� ������ 10 ������
    }

    void PlayMusic()
    {
        audioSource.clip = musicClips[currentClipIndex];
        audioSource.volume = 0; // ��������� ��������� 0
        audioSource.Play();
        StartCoroutine(FadeIn());
    }

    void ChangeMusic()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float startVolume = 0;
        float endVolume = 1;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = endVolume; // ���������� ������������� ���������
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float endVolume = 0;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = endVolume; // ���������� ������������� ���������
        currentClipIndex = (currentClipIndex + 1) % musicClips.Length; // ����������� �������
        PlayMusic();
    }
}

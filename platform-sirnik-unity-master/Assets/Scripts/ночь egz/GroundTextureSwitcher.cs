using System.Collections;
using UnityEngine;

public class GroundTextureSwitcher : MonoBehaviour
{
    public Sprite groundTexture1; // ������ ��������
    public Sprite groundTexture2; // ������ ��������
    public float switchInterval = 20f; // �������� ����� �������
    public float transitionDuration = 3f; // ������������ ����������

    private SpriteRenderer spriteRenderer;
    private bool isGroundTextureActive = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = groundTexture1; // ���������� ��������� ��������
        InvokeRepeating("SwitchTexture", switchInterval, switchInterval); // ������ ������������ �������
    }

    void SwitchTexture()
    {
        StartCoroutine(SmoothTextureTransition());
    }

    IEnumerator SmoothTextureTransition()
    {
        // ������� ������� � ������� �����
        Color startColor = spriteRenderer.color;
        Color endColor = Color.black; // ������ ����

        // ������� � �������
        for (float t = 0; t < 1; t += Time.deltaTime / transitionDuration)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        // ����� ��������
        spriteRenderer.sprite = isGroundTextureActive ? groundTexture2 : groundTexture1;
        isGroundTextureActive = !isGroundTextureActive;

        // ������� ������� ������� � ��������� �����
        for (float t = 0; t < 1; t += Time.deltaTime / transitionDuration)
        {
            spriteRenderer.color = Color.Lerp(endColor, startColor, t);
            yield return null;
        }
    }
}

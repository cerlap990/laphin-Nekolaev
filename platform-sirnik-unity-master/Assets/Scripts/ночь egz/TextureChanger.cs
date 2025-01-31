using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public SpriteRenderer sprite1;  // ������ SpriteRenderer
    public SpriteRenderer sprite2;  // ������ SpriteRenderer
    public float duration = 10f;    // ����������������� ����� �������

    private float elapsedTime = 0f;

    void Start()
    {
        // ���������� ������ SpriteRenderer ���������� ����������
        Color color = sprite2.color;
        color.a = 0f;
        sprite2.color = color;

        // ��������� �������� ��� ����� �������
        StartCoroutine(ChangeTextures());
    }

    System.Collections.IEnumerator ChangeTextures()
    {
        // ���� 10 ������
        yield return new WaitForSeconds(duration);

        // ������� ����� �������
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime);

            // ��������� ������������
            Color color1 = sprite1.color;
            Color color2 = sprite2.color;

            color1.a = 1 - t;  // ��������� ������������ ������� �������
            color2.a = t;      // ����������� ������������ ������� �������

            sprite1.color = color1;
            sprite2.color = color2;

            yield return null; // ���� ���������� �����
        }

        // ���������, ��� ����� ����������� ��������� � �����
        Color finalColor1 = sprite1.color;
        Color finalColor2 = sprite2.color;

        finalColor1.a = 0f;  // ������������ ������ ������ �� �����
        finalColor2.a = 1f;  // ������ ������ ��������� �����

        sprite1.color = finalColor1;
        sprite2.color = finalColor2;
    }
}

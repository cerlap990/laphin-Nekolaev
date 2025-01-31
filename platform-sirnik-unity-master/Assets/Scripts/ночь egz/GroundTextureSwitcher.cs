using System.Collections;
using UnityEngine;

public class GroundTextureSwitcher : MonoBehaviour
{
    public Sprite groundTexture1; // Первая текстура
    public Sprite groundTexture2; // Вторая текстура
    public float switchInterval = 20f; // Интервал смены текстур
    public float transitionDuration = 3f; // Длительность затемнения

    private SpriteRenderer spriteRenderer;
    private bool isGroundTextureActive = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = groundTexture1; // Установить начальную текстуру
        InvokeRepeating("SwitchTexture", switchInterval, switchInterval); // Начать переключение текстур
    }

    void SwitchTexture()
    {
        StartCoroutine(SmoothTextureTransition());
    }

    IEnumerator SmoothTextureTransition()
    {
        // Плавный переход к черному цвету
        Color startColor = spriteRenderer.color;
        Color endColor = Color.black; // Черный цвет

        // Переход к черному
        for (float t = 0; t < 1; t += Time.deltaTime / transitionDuration)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        // Смена текстуры
        spriteRenderer.sprite = isGroundTextureActive ? groundTexture2 : groundTexture1;
        isGroundTextureActive = !isGroundTextureActive;

        // Плавный переход обратно к исходному цвету
        for (float t = 0; t < 1; t += Time.deltaTime / transitionDuration)
        {
            spriteRenderer.color = Color.Lerp(endColor, startColor, t);
            yield return null;
        }
    }
}

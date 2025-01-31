using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public SpriteRenderer sprite1;  // Первый SpriteRenderer
    public SpriteRenderer sprite2;  // Второй SpriteRenderer
    public float duration = 10f;    // Продолжительность смены текстур

    private float elapsedTime = 0f;

    void Start()
    {
        // Установите второй SpriteRenderer изначально прозрачным
        Color color = sprite2.color;
        color.a = 0f;
        sprite2.color = color;

        // Запустите корутину для смены текстур
        StartCoroutine(ChangeTextures());
    }

    System.Collections.IEnumerator ChangeTextures()
    {
        // Ждем 10 секунд
        yield return new WaitForSeconds(duration);

        // Плавная смена текстур
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime);

            // Изменение прозрачности
            Color color1 = sprite1.color;
            Color color2 = sprite2.color;

            color1.a = 1 - t;  // Уменьшаем прозрачность первого спрайта
            color2.a = t;      // Увеличиваем прозрачность второго спрайта

            sprite1.color = color1;
            sprite2.color = color2;

            yield return null; // Ждем следующего кадра
        }

        // Убедитесь, что цвета установлены правильно в конце
        Color finalColor1 = sprite1.color;
        Color finalColor2 = sprite2.color;

        finalColor1.a = 0f;  // Подсвечиваем первый спрайт до конца
        finalColor2.a = 1f;  // Второй спрайт полностью видим

        sprite1.color = finalColor1;
        sprite2.color = finalColor2;
    }
}

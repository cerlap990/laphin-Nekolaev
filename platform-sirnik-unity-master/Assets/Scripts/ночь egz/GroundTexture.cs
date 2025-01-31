using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTexture : MonoBehaviour
{
    public Sprite groundTexture1;
    public Sprite groundTexture2;
    public float switchInterval = 20f;
    public float transitionDuration = 3f;

    private SpriteRenderer spriteRenderer;
    private bool isGroundTextureActive = true;
    private static List<GroundTexture> instances = new List<GroundTexture>();

    void Awake()
    {
        instances.Add(this);
        if (instances.Count == 2)
        {
            InvokeRepeating("ChangeTextures", switchInterval, switchInterval);
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = groundTexture1;
        StartCoroutine(SwitchTexturesAfterDelay());
    }

    void OnDestroy()
    {
        instances.Remove(this);
    }

    private IEnumerator SwitchTexturesAfterDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);
            ChangeTextures();
        }
    }

    void ChangeTextures()
    {
        foreach (var instance in instances)
        {
            instance.StartCoroutine(instance.SmoothTextureTransition());
        }
    }

    IEnumerator SmoothTextureTransition()
    {
        Color startColor = spriteRenderer.color;
        Color endColor = Color.black;

        // Fade out
        for (float t = 0; t < 1; t += Time.deltaTime / transitionDuration)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        // Change the texture
        spriteRenderer.sprite = isGroundTextureActive ? groundTexture2 : groundTexture1;
        isGroundTextureActive = !isGroundTextureActive;

        // Fade in
        for (float t = 0; t < 1; t += Time.deltaTime / transitionDuration)
        {
            spriteRenderer.color = Color.Lerp(endColor, startColor, t);
            yield return null;
        }
    }
}

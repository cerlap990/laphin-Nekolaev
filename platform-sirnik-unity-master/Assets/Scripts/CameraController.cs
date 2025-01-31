using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public Texture2D backgroundTexture; // Замените "backgroundTexture" названием вашего текстурного изображения

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        // Устанавливаем флаг камеры для отображения текстуры в качестве заднего фона
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
   

        // Установите текстуру в качестве фона
        if (backgroundTexture != null)
        {
            Material mat = new Material(Shader.Find("Unlit/Texture"));
            mat.mainTexture = backgroundTexture;
            GetComponent<Renderer>().material = mat;
        }

        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Установите позицию трансформа камеры так же, как у игрока, но с заданным смещением.
        Vector3 aux = player.transform.position + offset;
        aux.y = 0;
        transform.position = aux;
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}

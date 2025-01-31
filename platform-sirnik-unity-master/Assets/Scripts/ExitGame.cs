using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Проверяем, нажата ли клавиша Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Выход из игры
            Application.Quit();

            // Если вы находитесь в редакторе Unity, используйте следующую строку для остановки режима игры
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}

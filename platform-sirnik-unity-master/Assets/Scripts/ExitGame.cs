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
        // ���������, ������ �� ������� Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ����� �� ����
            Application.Quit();

            // ���� �� ���������� � ��������� Unity, ����������� ��������� ������ ��� ��������� ������ ����
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}

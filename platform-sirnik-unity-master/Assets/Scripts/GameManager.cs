using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cactus"))
        {
            isGameOver = true;
            FindObjectOfType<HUD>().GameOver(); // Вызываем метод GameOver в HUD
            // Здесь можно добавить логику для перезапуска игры
        }
    }
}

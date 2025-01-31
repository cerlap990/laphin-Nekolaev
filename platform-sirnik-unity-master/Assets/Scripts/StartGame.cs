using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void BeginGame() // Переименуйте метод здесь
    {
        SceneManager.LoadScene("Game");
    }
}

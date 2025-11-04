using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player has reached the finish line!");
            SceneManager.LoadScene("Credits");  // Use scene name or correct index
        }
    }
}

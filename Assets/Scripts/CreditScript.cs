using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for scene loading

public class CreditScript : MonoBehaviour
{
    public float scrollSpeed = 20f; // Speed at which the credits scroll
    public float duration = 300f;   
    private RectTransform rectTransform;
    private float timer = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Scroll the credits
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        // Count elapsed time
        timer += Time.deltaTime;

        // After duration, load Main Menu scene
        if (timer >= duration)
        {
            SceneManager.LoadScene("Menu Scene"); // Replace with your Main Menu scene name
        }
    }
}

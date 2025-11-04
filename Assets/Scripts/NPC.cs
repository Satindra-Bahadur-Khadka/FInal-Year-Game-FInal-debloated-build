using System.Collections;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialougePanel;
    public TMP_Text dialougeText;
    public string[] dialouge;
    private int index;

    public GameObject continueButton;
    public float wordSpeed;
    public bool isPlayerInRange;

    private bool isTyping = false; // Tracks if coroutine is running

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange)
        {
            if (dialougePanel.activeInHierarchy)
            {
                // If typing is still running, skip to full text
                if (isTyping)
                {
                    StopAllCoroutines();
                    dialougeText.text = dialouge[index];
                    isTyping = false;
                    continueButton.SetActive(true);
                }
                else
                {
                    NextLine();
                }
            }
            else
            {
                dialougePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
    }

    public void ZeroText()
    {
        dialougeText.text = "";
        index = 0;
        dialougePanel.SetActive(false);
        continueButton.SetActive(false);
    }

    IEnumerator Typing()
    {
        isTyping = true;
        dialougeText.text = "";
        foreach (char letter in dialouge[index].ToCharArray())
        {
            dialougeText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isTyping = false;
        continueButton.SetActive(true);
    }

    public void NextLine()
    {
        continueButton.SetActive(false);

        if (index < dialouge.Length - 1)
        {
            index++;
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            ZeroText();
        }
    }
}

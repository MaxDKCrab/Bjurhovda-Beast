using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenStories : MonoBehaviour
{
    // [SerializeField] private InteractionManager interact;
    [SerializeField] private CharacterController movement;
    [SerializeField] private MouseLook mouse;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Animator anim;
    public float waitForSeconds;
    

    private bool withinRange;
    private bool isReading;
    [TextArea(10, 10)] public string storyText;

    private void OnTriggerEnter(Collider other)
    {
        // interact.ShowInteractMessage("Press E to read");
        withinRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        // interact.HideInteractMessage();
        withinRange = false;
        
    }
    private void Update()
    {
        if (!withinRange)
        {
            return;
        }
        
        OpenStory();
    }

    public void OpenStory()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isReading)
        {
            movement.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            mouse.enabled = false;
            isReading = true;
            // interact.HideInteractMessage();
            anim.SetTrigger("StoriesIn");
            StartCoroutine(TypeSentence(storyText));
        }
        else if (Input.GetKeyDown(KeyCode.E) && isReading)
        {
            isReading = false;
            movement.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            mouse.enabled = true;
            anim.SetTrigger("StoriesOut");
            StopAllCoroutines();
        }
    }
    
    IEnumerator TypeSentence (string sentence)
    {
        text.text = "";
        yield return new WaitForSeconds(0.5f);
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(waitForSeconds);
        }
    }
}

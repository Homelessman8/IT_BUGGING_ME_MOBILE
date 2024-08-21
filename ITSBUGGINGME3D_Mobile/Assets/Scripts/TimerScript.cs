using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public static TimerScript inst;     // Static reference to the TimerScript instance.

    [Header("UI feedback")]
    public TextMeshProUGUI timeFeedback; // Reference to the TextMeshProUGUI component for displaying time.

    // Start is called before the first frame update
    void Start()
    {
        // Check if there is already an instance of TimerScript.
        if (inst)
        {
            Debug.LogWarning("There is more than 1 TimerScript in the scene");
        }
        inst = this; // Set this as the instance.
        StartCoroutine(TimeTick()); // Start the timer coroutine.
    }

    // Update is called once per frame
    void Update()
    {
        // This Update method is currently empty.
        // You can add additional functionality here if needed.
    }

    // Timer Coroutine
    // Calculates the in-game time and updates the displayed time.
    IEnumerator TimeTick(int sec = 0, int min = 0)
    {
        while (Time.timeScale != 0)
        {
            yield return new WaitForSeconds(1);

            // Update seconds and minutes.
            if (sec == 59)
            {
                sec = 0;
                min++;
            }
            else
            {
                sec++;
            }

            // Format and display the time in the UI.
            if (sec < 10)
            {
                timeFeedback.text = min + ":0" + sec;
            }
            else
            {
                timeFeedback.text = min + ":" + sec;
            }
        }

        // Stop the TimeTick coroutine when the game is paused.
        StopCoroutine(TimeTick());
    }
}
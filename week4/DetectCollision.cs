using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DetectCollision : MonoBehaviour
{
    private const string StartingSceneName = "week3"; // name of the game's initial scene.
    int score = 0;
    private const string ScoreKey = "HighScore"; // Define a constant key to be saved for PlayerPrefs
    GameObject messageUI;
    TextMeshProUGUI messageText;
    bool startDisplayTimer;
    float displayTimer;
    GameObject box1, box2, box3, box4;
    int nbBoxesCollected = 0;

    void DisplayMessage(string message)
    {
        if (messageText != null)
            messageText.text = message;
    }

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name; //Get the current scene name ONCE and store it in a single variable.
        
        messageUI = GameObject.Find("messageUI");
        if (messageUI == null)
        {
            Debug.LogWarning("messageUI GameObject not found. Make sure a GameObject named 'messageUI' exists in the scene.");
        }
        else
        {
            messageText = messageUI.GetComponent<TextMeshProUGUI>();
            if (messageText == null)
                Debug.LogWarning("TextMeshProUGUI component not found on messageUI GameObject.");
        }

        // Provide an initial message (empty string to clear)
        DisplayMessage("");

        startDisplayTimer = false;
        displayTimer = 0.0f;

        box1 = GameObject.Find("item1");
        box2 = GameObject.Find("item2");
        box3 = GameObject.Find("item3");
        box4 = GameObject.Find("item4");

        box1.SetActive(false);
        box2.SetActive(false);
        box3.SetActive(false);
        box4.SetActive(false);

        // Check if the current scene is the starting scene of the game.
        if (currentSceneName == StartingSceneName)
        {
            // If it's the first scene, reset the saved score to 0.
            PlayerPrefs.SetInt(ScoreKey, 0);
            PlayerPrefs.Save();
        }

        // Else Load the score from PlayerPrefs (eg load the score saved from the previous level) If it was just reset, it will be 0. 
        score = PlayerPrefs.GetInt(ScoreKey, 0);
        Debug.Log("Loaded Score: " + score + " in scene: " + currentSceneName);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string nameOfObject = hit.gameObject.name;
        string tagOfTheObject = hit.gameObject.tag;
        
        if( tagOfTheObject == "toCollect")
        {
            startDisplayTimer = true;
            // Debug.Log("Just collided with" + nameOfObject);
            DisplayMessage("Just Collided with " + nameOfObject);
            nbBoxesCollected = nbBoxesCollected + 1;
            if (nbBoxesCollected == 1)
            {
                box1.SetActive(true);
            }
            if (nbBoxesCollected == 2)
            {
                box2.SetActive(true);
            }
            if (nbBoxesCollected == 3)
            {
                box3.SetActive(true);
            }
            if (nbBoxesCollected == 4)
            {
                box4.SetActive(true);
            }
            Destroy(hit.gameObject);
            score = score + 1;
            

            PlayerPrefs.SetInt(ScoreKey, score); // Save the updated score immediately
            PlayerPrefs.Save(); // PlayerPrefs.Save() writes the changes to disk. It's good practice to call it 

            //Debug.Log("Score: " + score);
            
            if (score == 4)
            {
                DisplayMessage("You've collected 4 boxes, Lets go to the next level");
                ChangeLevel();
            }

            if (score == 8)
            {
                DisplayMessage("You've collected 8 boxes. Lets go to the final level");
                ChangeToFinalLevel();
            }
        }
    }

    void ChangeLevel()
    {
        SceneManager.LoadScene("week3level2");
    }

    void ChangeToFinalLevel()
    {
        SceneManager.LoadScene("week3level3");
    }

    // Update is called once per frame
    void Update()
    {
        if (startDisplayTimer == true)
        {
            displayTimer = displayTimer + Time.deltaTime;
            if (displayTimer >= 2)
            {
                if (messageText != null)
                    messageText.text = "";
                startDisplayTimer = false;
                displayTimer = 0.0f;
            }
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectCollision : MonoBehaviour
{
    private const string StartingSceneName = "week3"; // name of the game's initial scene.
    int score = 0;
    private const string ScoreKey = "HighScore"; // Define a constant key to be saved for PlayerPrefs

    void Start()
        {
            string currentSceneName = SceneManager.GetActiveScene().name; //Get the current scene name ONCE and store it in a single variable.

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
            // Debug.Log("Just collided with" + nameOfObject);
            Destroy(hit.gameObject);
            score = score + 1;
            

            PlayerPrefs.SetInt(ScoreKey, score); // Save the updated score immediately
            PlayerPrefs.Save(); // PlayerPrefs.Save() writes the changes to disk. It's good practice to call it 

            Debug.Log("Score: " + score);
            
            if (score == 4)
            {
                Debug.Log("You've collected 4 boxes, Lets go to the next level");
                ChangeLevel();
            }

            if (score == 8)
            {
                Debug.Log("You've collected 8 boxes. Lets go to the final level");
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
        
    }
}
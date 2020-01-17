using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenNewLevel : MonoBehaviour
{
    [SerializeField]
    private int levelToOpenId = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (levelToOpenId == -1)
            Debug.LogError("Level id must be positive");

        if (levelToOpenId > SceneManager.sceneCountInBuildSettings - 1)
            Debug.LogError("Level id is bigger then total scenes amount");
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(levelToOpenId);
    }
}

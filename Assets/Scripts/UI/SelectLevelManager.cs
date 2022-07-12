using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevelManager : MonoBehaviour
{
    int levelId = 0;
    // Start is called before the first frame update
    void Start()
    {
        levelId = PlayerPrefs.GetInt("level_id");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectLevel(int id){
        levelId = id;
        PlayerPrefs.SetInt("level_id", id);
        SceneManager.LoadScene("MainMenu");
    }
}

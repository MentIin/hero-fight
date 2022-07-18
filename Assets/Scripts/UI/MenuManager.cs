using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite[] backgrounds;
    public Sprite[] heroesPics;
    public GameObject pickHeroButtonBlue;
    public GameObject pickHeroButtonRed;
    public int heroCol=2;
    private int bluePick;
    private int redPick;
    private int levelId;
    public Sprite[] levelIcons;
    void Awake()
    {
        if (!PlayerPrefs.HasKey("level_id")) PlayerPrefs.SetInt("level_id", 0);
        levelId = PlayerPrefs.GetInt("level_id");
    }
    // Start is called before the first frame update
    void Start()
    {
        backgroundImage.sprite = backgrounds[levelId];
        if (!PlayerPrefs.HasKey("blue_hero_choice")){
            bluePick = Random.Range(0, heroCol);
            redPick =  Random.Range(0, heroCol);
        }else{
            bluePick = PlayerPrefs.GetInt("blue_hero_choice");
            redPick = PlayerPrefs.GetInt("red_hero_choice");
        }
        PlayerPrefs.SetInt("blue_hero_choice", bluePick);
        PlayerPrefs.SetInt("red_hero_choice", redPick);
        SetPickIcons();
        pickHeroButtonBlue.GetComponent<Image>().sprite = heroesPics[bluePick];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame(){
        PlayerPrefs.SetInt("blue_hero_choice", bluePick);
        PlayerPrefs.SetInt("red_hero_choice", redPick);
        SceneManager.LoadScene("GameScene");
    }
    public void BlueChange(){
        bluePick++;
        if (bluePick >= heroCol){
            bluePick = 0;
        }
        SetPickIcons();
    }
    public void RedChange(){
        redPick++;
        if (redPick >= heroCol){
            redPick = 0;
        }
        SetPickIcons();
    }
    void SetPickIcons(){
        pickHeroButtonBlue.GetComponent<Image>().sprite = heroesPics[bluePick];
        pickHeroButtonRed.GetComponent<Image>().sprite = heroesPics[redPick];
    }
    public void ToSelectMap(){
        PlayerPrefs.SetInt("blue_hero_choice", bluePick);
        PlayerPrefs.SetInt("red_hero_choice", redPick);
        SceneManager.LoadScene("SelectMap");
    }
}

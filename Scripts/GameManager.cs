using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject[] maps;
    public GameObject[] heroes;
    public float secondsWaintForstart=2f;
    public MovementController movementController;
    public GameObject menuCanvas;
    public GameObject[] bonuses;
    public Transform[] bonusesSpawnPoints;
    public GameObject blueHero;
    public GameObject redHero;
    public GameObject gameEndText;
    private HeroMovement blueMovement;
    private HeroMovement redMovement;
    public Image blueHealthbar;
    public Image redHealthbar;
    public GameObject blueSkillButon;
    public GameObject redSkillButton;
    public static GameManager S;
    public int bonusesCount=0;
    public Transform blueHeroSpawnpoint;
    public Transform redHeroSpawnpoint;
    public Diamond blueDiamond;
    public Diamond redDiamond;
    private AudioSource audioSource;
    void Awake()
    {
        GameObject map = maps[PlayerPrefs.GetInt("level_id")];
        Instantiate(map, map.transform.position, map.transform.rotation);
        FindSpawnPoints();
        GameObject blueHeroPref = heroes[PlayerPrefs.GetInt("blue_hero_choice")];
        GameObject redHeroPref = heroes[PlayerPrefs.GetInt("red_hero_choice")];
        blueHero = Instantiate(blueHeroPref, blueHeroSpawnpoint);
        redHero = Instantiate(redHeroPref, redHeroSpawnpoint);
        blueMovement = blueHero.GetComponent<HeroMovement>();
        redMovement = redHero.GetComponent<HeroMovement>();
        S = this;
        blueSkillButon.GetComponent<SkilButton>().myHero = blueMovement;
        redSkillButton.GetComponent<SkilButton>().myHero = redMovement;
        movementController.heroBlue = blueHero;
        movementController.heroRed = redHero;
        redMovement.red = true;
        blueDiamond.hero = blueHero;
        redDiamond.hero = redHero;
        audioSource = GetComponent<AudioSource>();
        ContinueGame();
    }



    void FindSpawnPoints(){
        blueHeroSpawnpoint = GameObject.Find("BlueHeroSpawn").transform;
        redHeroSpawnpoint = GameObject.Find("RedHeroSpawn").transform;
        GameObject[] bonusesSpawnPointsGm = GameObject.FindGameObjectsWithTag("BonusSpawnPoint");
        bonusesSpawnPoints = new Transform[bonusesSpawnPointsGm.Length];
        for(int i=0; i<bonusesSpawnPoints.Length;i++){
            bonusesSpawnPoints[i] = bonusesSpawnPointsGm[i].transform;
        }
    }
    void StartGame(){
        InvokeRepeating("TrySpawnBonus", secondsWaintForstart, 10f);
        movementController.StartGame();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", 3f);
    }

    // Update is called once per frame
    void Update()
    {

        blueHealthbar.fillAmount =  (float)blueMovement.heatpoints / (float)blueMovement.maxHeatpoints;
        redHealthbar.fillAmount =  (float)redMovement.heatpoints / (float)redMovement.maxHeatpoints;
    }

    public void GameEnd(bool red){
        gameEndText.SetActive(true);
        Text tx = gameEndText.GetComponent<Text>();
        // if died hero is not red
        if (!red){
            tx.text = "red won!";
            tx.color = Color.red;
        }else{
            tx.color = Color.blue;
            tx.text = "blue won!";
        }
       StartCoroutine( LoadScene(3, "GameScene") );   
    }
    IEnumerator LoadScene(float sec, string name){
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(name);
    }
    void TrySpawnBonus(){
        
        
        if (bonusesCount == 0){
            SpawnBonus();
        }
    
    }
    void SpawnBonus(){
        GameObject bonus = bonuses[Random.Range(0, bonuses.Length)];
        Transform spawnPoint = bonusesSpawnPoints[Random.Range(0, bonusesSpawnPoints.Length)];
        Instantiate(bonus, spawnPoint.position, bonus.transform.rotation);
    }
    public void Pause(){
        Time.timeScale = 0;
        menuCanvas.SetActive(true);
        audioSource.volume = 0.4f;
    }
    public void ContinueGame(){
        Time.timeScale = 1;
        menuCanvas.SetActive(false);
        audioSource.volume = 0.7f;
    }
    public void ReturnToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject heroBlue;
    public GameObject heroRed;
    public HeroMovement heroBlueMovement;
    public HeroMovement heroRedMovement;

    private Rigidbody2D blueHeroRb;

    private Rigidbody2D redHeroRb;
    // Start is called before the first frame update
    void Start()
    {
        heroBlueMovement = heroBlue.GetComponent<HeroMovement>();
        if (mode == 0)
        {
            heroRedMovement = heroRed.GetComponent<HeroMovement>();
        }
        
    }
    public void StartGame()
    {
        heroBlueMovement.StartMove();
        if (mode == 0)
        {
            heroRedMovement.StartMove();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            heroBlueMovement.StartMove();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            heroBlueMovement.Stop();
        }
        if (Input.GetAxis("Horizontal") > 0){
            BlueHeroUseSkill();
        }else if (Input.GetAxis("Horizontal") < 0){
            BlueHeroJump();
        }
        if (Input.GetAxis("Vertical") < 0){
            RedHeroUseSkill();
        }else if (Input.GetAxis("Vertical") > 0){
            RedHeroJump();
        }
    }
    public void BlueHeroJump()
    {
        heroBlueMovement.Jump();
    }
    public void BlueHeroUseSkill()
    {
        heroBlueMovement.UseSkill();
    }
    

    public void RedHeroStopJump()
    {
        
    }
    public void RedHeroJump()
    {
        heroRedMovement.Jump();
    }
    public void RedHeroUseSkill()
    {
        heroRedMovement.UseSkill();
    }
    public void BlueStopJump()
    {
        heroBlueMovement.EndJump();
    }
    public void RedStopJump()
    {
        heroRedMovement.EndJump();
    }

    private int mode
    {
        get
        {
            return PlayerPrefs.GetInt("mode");
        }
    }
}

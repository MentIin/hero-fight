using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject heroBlue;
    public GameObject heroRed;
    public HeroMovement heroBlueMovement;
    public HeroMovement heroRedMovement;
    // Start is called before the first frame update
    void Start()
    {
        heroBlueMovement = heroBlue.GetComponent<HeroMovement>();
        heroRedMovement = heroRed.GetComponent<HeroMovement>();
    }
    public void StartGame()
    {
        heroBlueMovement.StartMove();
        heroRedMovement.StartMove();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [SerializeField] private float crystalDuration;
    [SerializeField] public GameObject crystalPrefab;
    private GameObject currentCrystal;

    [Header("Explosive Crystal")]
    [SerializeField] private bool canExplode;

    [Header("Moving Crystal")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;

    public override void UseSkill()
    {
        base.UseSkill();

        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            Crystal_Skill_Controller currentCrystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

            currentCrystalScript.SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(currentCrystal.transform));
        }
        else
        {
            if (canMoveToEnemy)
                return;

                
            Vector2 playerPos = player.transform.position;

            player.transform.position = currentCrystal.transform.position; //F tuşuna 2. kez bastığımızda kristale ışınlanıyoruz

            currentCrystal.transform.position = playerPos;

            currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ClaymoreAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        //Build placeMine = parent.GetComponentInChildren<Build>();
        Build placeMine = GameObject.Find("MinePlacer").GetComponent<Build>();
        placeMine.Place();

        if (PlayerPrefs.HasKey("Mine"))
        {
            int Mine = PlayerPrefs.GetInt("Mine");
            Mine++;
            PlayerPrefs.SetInt("Mine", Mine);
        }
        else
        {
            PlayerPrefs.SetInt("Mine", 1);
        }
        PlayerPrefs.Save();
    }

    public override void BeginCooldown(GameObject parent)
    {
        
    }
}

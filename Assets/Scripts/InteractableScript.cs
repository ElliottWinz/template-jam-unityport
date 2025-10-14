using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int scoreValue = 1;
    private bool collected = false;
    [SerializeField] int itemType;
    void Start()
    {
        
    }

    public void CollectItem(PlayerScript plr)
    {

        if (collected)
        {

            return;
        }
        if (itemType == 0)
        {
            plr.awardPoints(scoreValue);
            collected = false;
            Destroy(this.gameObject);
        }

    }
}

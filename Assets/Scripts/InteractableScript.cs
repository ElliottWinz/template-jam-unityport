using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float decayReversalAmount = 0.25f;
    private bool collected = false;
    [SerializeField] int itemType;
    void Start()
    {   
        
    }

    public void CollectItem(PlayerScript player)
    {

        if (collected)
        {

            return;
        }
        if (itemType == 0)
        {
            player.ReverseDecay(decayReversalAmount);
            collected = false;
            Destroy(this.gameObject);
        }

    }
}

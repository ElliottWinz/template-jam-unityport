using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int decayReversalAmount = 1;
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

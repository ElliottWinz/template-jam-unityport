using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int decayValue = 1;
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
            plr.ReverseDecay(decayValue);
            collected = false;
            Destroy(this.gameObject);
        }

    }
}

﻿using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{
    private enum PickUpType
    {
        Weapon,
        Signature,
        Victim
    };
    private PickUpType Type;
    private string Name;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }

}

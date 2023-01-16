using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugInstantiater : Interactable
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private TextMeshProUGUI worldText;

    private void Start()
    {
        worldText.text = prefabToSpawn.name;
    }

    protected override void Interact()
    {
        Instantiate(prefabToSpawn, spawnLocation.position, spawnLocation.rotation);
    }
}

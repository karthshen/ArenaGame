using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour
{
    public List<GameObject> pickupRespawnLocations;

    [SerializeField]
    private float ITEM_GENERATE_INTERVAL = 5.0f;

    private float currentTime = 0f;

    private List<string> pickupItems;

    // Use this for initialization
    void Start()
    {
        pickupItems = new List<string>();

        pickupItems.Add("EnergyPotion");
        pickupItems.Add("HealthPotion");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= ITEM_GENERATE_INTERVAL)
        {
            ItemGenerate();
            currentTime = 0;
        }
    }

    private void ItemGenerate()
    {
        GameObject newPickup = Object.Instantiate(Resources.Load(pickupItems[Random.Range(0, pickupItems.Count)])) as GameObject;

        newPickup.transform.position = pickupRespawnLocations[Random.Range(0, pickupRespawnLocations.Count)].transform.position;
    }
}

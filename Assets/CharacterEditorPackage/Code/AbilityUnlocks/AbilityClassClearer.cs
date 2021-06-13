using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityClassClearer : MonoBehaviour
{
    void OnTriggerEnter(Collider a_Collider)
    {
        AbiltityUnlockManager unlockManager = a_Collider.GetComponent<AbiltityUnlockManager>();

        unlockManager.ClearAbilities();
    }
}

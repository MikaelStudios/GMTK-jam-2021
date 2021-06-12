using UnityEngine;
using System.Collections;
//--------------------------------------------------------------------
//When the player enters, remove an ability class from the ability unlock manager
//--------------------------------------------------------------------
public class AbilityDamageTrigger : MonoBehaviour {


    void OnTriggerEnter(Collider a_Collider)
    {
        ControlledCapsuleCollider controlledCapsuleCollider = a_Collider.GetComponent<ControlledCapsuleCollider>();
        if (!controlledCapsuleCollider)
            return;
        AbiltityUnlockManager abiltityUnlockManager = a_Collider.GetComponent<AbiltityUnlockManager>();
        if(!abiltityUnlockManager)
            return;
        //Prevent damage state to be used if the collider is no-clipping
        if (!controlledCapsuleCollider.AreCollisionsActive())
            return;
        Debug.Log("Damage triggered by: " + transform.name);

        //Returns false if no ability is left
        if(abiltityUnlockManager.RemoveRandomAbilityClass())
        {
            //Throw away player
            Vector2 sourceToCollider = a_Collider.transform.position - transform.position;
            controlledCapsuleCollider.SetVelocity(sourceToCollider.normalized * controlledCapsuleCollider.GetVelocity().magnitude * .95f);
        }
        else
        {
            Debug.Log("Player killed!");
            if (InSceneLevelSwitcher.Get())
            {
                InSceneLevelSwitcher.Get().Respawn();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour
{
    public float m_Cooldown = 10.0f;
    private float timeValue;
    private GameObject spawnEntity;

    void Start()
    {
        timeValue = m_Cooldown;
        spawnEntity = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawnEntity.activeSelf)
        {
            if(timeValue > 0.0f)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                spawnEntity.SetActive(true);
                timeValue = m_Cooldown;
                Respawn();
            }
        }
    }

    void Respawn()
    {
        //...
    }
}

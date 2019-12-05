using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthDisplay : MonoBehaviour
{
    TMP_Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var player = FindObjectOfType<Player>();
        if (!player)
        {
            healthText.text = "0";
        }
        else
        {
            healthText.text = player.getHealth().ToString();
        }
    }
}

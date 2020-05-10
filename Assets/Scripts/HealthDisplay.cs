using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.SetText(player.GetHealth().ToString());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.SetText(gameSession.GetScore().ToString("00000"));
    }
}

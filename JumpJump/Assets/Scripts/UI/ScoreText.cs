using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        GameManager.Instance.OnScoreChanged.AddListener(UpdateText);
    }
    public void UpdateText(int score)
    {
        // _ui의 텍스트를 수정
        _ui.text = $"{score}";
    }
}
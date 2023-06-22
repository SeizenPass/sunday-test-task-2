using System;
using TMPro;
using UnityEngine;

namespace Project.Scripts
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fpsText;

        private void Update()
        {
            if (fpsText) fpsText.text = $"FPS: {(int)(1f / Time.unscaledDeltaTime)}";
        }
    }
}
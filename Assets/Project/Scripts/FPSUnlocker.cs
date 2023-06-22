using UnityEngine;

namespace Project.Scripts
{
    public class FPSUnlocker : MonoBehaviour
    {
        public void SetTargetFramerate(int fps)
        {
            if (fps <= 0) return;
            Application.targetFrameRate = fps;
        }
    }
}
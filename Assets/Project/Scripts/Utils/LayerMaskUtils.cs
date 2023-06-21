using UnityEngine;

namespace Project.Scripts.Utils
{
    public class LayerMaskUtils
    {
        public static bool CompareLayerMasks(LayerMask containerLayerMask, LayerMask targetLayerMask) {
            return CompareLayers(containerLayerMask.value, targetLayerMask.value);
        }

        public static bool CompareLayers(int containerLayerMask, int targetLayerMask) {
            return containerLayerMask == (containerLayerMask | (1 << targetLayerMask));
        }

        public static int FromLayerMaskToInteger(LayerMask layerMask)
        {
            return (int)Mathf.Log(layerMask.value, 2);
        }
    }
}
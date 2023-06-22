using System.Collections;
using Project.Scripts.Pool;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class FootprintGenerator : MonoBehaviour
    {
        [SerializeField] private ObjectPool footPrintPool;
        [SerializeField] private Transform player;
        [SerializeField] private float lifetime = 6f;
        

        public void LeaveFootprint(Vector3 pos)
        {
            var ftp = footPrintPool.GetPooledObject();

            ftp.transform.position = pos;
            ftp.transform.rotation = player.rotation;
            ftp.SetActive(true);
            StartCoroutine(FootprintLifetime(ftp));
        }

        private IEnumerator FootprintLifetime(GameObject ftp)
        {
            yield return new WaitForSeconds(lifetime);
            ftp.SetActive(false);
        }
    }
}
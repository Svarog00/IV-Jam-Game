using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Gameplay.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public void Follow(GameObject hero)
        {
            GetComponentInChildren<CinemachineVirtualCamera>().Follow = hero.transform;
        }
    }
}
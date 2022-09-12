using UnityEngine;

namespace Platformer.View
{

    public class BackgroundParallax : MonoBehaviour
    {
        public Vector3 movementScale = Vector3.one;

        Transform _camera;

        void Awake()
        {
            _camera = Camera.main.transform;
        }

        void LateUpdate()
        {
            Vector3 temp = Vector3.Scale(_camera.position, movementScale);
            temp.z = transform.position.z;
            transform.position = temp;
        }

    }
}
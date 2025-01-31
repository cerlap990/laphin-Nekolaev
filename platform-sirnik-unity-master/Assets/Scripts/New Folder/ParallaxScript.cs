using UnityEngine;

namespace KaizoTrap
{
    public class ParallaxScript : MonoBehaviour
    {
        private float startPos, length;
        public new GameObject camera;
        public float parallaxEffect;
        public float smoothSpeed = 0.125f; // Для настройки скорости сглаживания
        private Vector3 targetPosition;

        void Start()
        {
            startPos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
            targetPosition = transform.position;
        }

        void Update()
        {
            float temp = camera.transform.position.x * (1 - parallaxEffect);
            float dist = camera.transform.position.x * parallaxEffect;

            targetPosition = new Vector3(startPos + dist, transform.position.y, transform.position.z);

            // Плавно переходим к целевой позиции для параллакса
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            if (temp > startPos + length)
                startPos += length;
            else if (temp < startPos - length)
                startPos -= length;
        }
    }
}

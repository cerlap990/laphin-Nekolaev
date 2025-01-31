using UnityEngine;

namespace KaizoTrap
{
    public class FixedFollow : MonoBehaviour
    {
        public Transform target;                // Трансформ игрока
        public Vector3 cameraOffset;            // Смещение камеры относительно игрока
        public float smoothSpeed = 0.125f;      // Скорость сглаживания
        public float backgroundSmoothSpeed = 0.05f; // Скорость сглаживания фона

        private Transform _transform;

        void Awake()
        {
            _transform = gameObject.transform;
        }

        void LateUpdate()
        {
            UpdateCameraPosition();
            SmoothBackground(); // Вызов функции для сглаживания фона
        }

        void UpdateCameraPosition()
        {
            // Рассчитываем желаемую позицию камеры
            Vector3 desiredPosition = target.position + cameraOffset;
            // Плавно переходим к желаемой позиции
            Vector3 smoothedPosition = Vector3.Lerp(_transform.position, desiredPosition, smoothSpeed);
            _transform.position = smoothedPosition;
        }

        void SmoothBackground()
        {
            // Предполагаем, что у вас есть фоновый объект, который нужно сгладить
            GameObject backgroundObject = GameObject.Find("Background");
            if (backgroundObject != null)
            {
                // Сглаживаем позицию фона (вы можете изменить логику по вашему усмотрению)
                Vector3 backgroundPosition = new Vector3(target.position.x, target.position.y, backgroundObject.transform.position.z);
                backgroundObject.transform.position = Vector3.Lerp(backgroundObject.transform.position, backgroundPosition, backgroundSmoothSpeed);
            }
        }
    }
}

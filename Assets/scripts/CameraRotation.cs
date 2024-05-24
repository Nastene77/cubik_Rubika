using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform targetPos; //целевая позиция
    [SerializeField] private int sensivity = 3; //чувствительность для вращения и движения
    [SerializeField] private float scrollSpeed = 1f; //скорость прокрутки колеса мыши для изменения приближения.
    [SerializeField] private int maxdistance = 20; // максимальное расстояние от цели.
    [SerializeField] private int mindistance = 1; // миниальное расстояние от цели.

    private void LateUpdate()
    {
        //ВРАЩЕНИЕ ВОКРУГ ЦЕНТРАЛЬНОЙ ТОЧКИ УСТАНОВКИ С ЗАЖАТОЙ ПРАВОЙ КЛАВИШИ МЫШИ
        if (Input.GetMouseButton(1))
        {
            float horizontal = Input.GetAxis("Mouse X") * sensivity; //значение горизонтального движения мыши
            float vertical = -Input.GetAxis("Mouse Y") * sensivity;
            if (horizontal != 0) transform.RotateAround(targetPos.position, Vector3.up, horizontal); //вращение объекта
            if (vertical != 0) transform.RotateAround(targetPos.position, transform.right, vertical); //вращение объекта
        }

        // ПРИБЛИЖЕНИЕ И УДАЛЕНИЕ КАМЕРЫ К УСТАНОВКЕ НА СЦЕНЕ ПРОКРУТКОЙ КОЛЕСА МЫШИ
        float z = Input.GetAxis("Mouse ScrollWheel"); //получения значения прокрутки колеса мыши
        if (z != 0)
        {
            Vector3 offset = transform.position - targetPos.position; // находим текущее смещение камеры от целевого объекта
            float distance = offset.magnitude; // находим текущее расстояние до целевого объекта
            distance -= z * scrollSpeed; // изменяем расстояние к целевому объекту
            distance = Mathf.Clamp(distance, mindistance, maxdistance); // ограничиваем значение расстояния
            offset = offset.normalized * distance; // обновляем смещение с учетом нового расстояния
            transform.position = targetPos.position + offset; // устанавливаем новое положение камеры
        }
    }
}
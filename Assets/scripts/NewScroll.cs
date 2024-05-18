using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScroll : MonoBehaviour
{
    [SerializeField]
    Transform targetPos;//целевая позиция
    [SerializeField]
    int sensivity = 3;//чувствительность для вращения и движения
    [SerializeField]
    float scrollSpeed = 1f;//скорость прокрутки колеса мыши для изменения приближения.
    [SerializeField]
    int maxdistance = 20;// максимальное расстояние от цели.
    [SerializeField]
    int mindistance = 1;// миниальное расстояние от цели.


    void Update()
    {//ВРАЩЕНИЕ ВОКРУГ ЦЕНТРАЛЬНОЙ ТОЧКИ УСТАНОВКИ С ЗАЖАТОЙ ПРАВОЙ КЛАВИШИ МЫШИ
        if (Input.GetMouseButton(1))
        {
            float yy = Input.GetAxis("Mouse X") * sensivity;  //значение горизонтального движения мыши
            Debug.Log(transform.rotation.y);                    //y угла поворота объекта transform в консоль отладки
            if (yy != 0)
            {
                transform.RotateAround(targetPos.position, Vector3.up, yy); //вращение объекта, градусов yy
            }

        }
        //Горизонтальное и вертикальное движение камеры
        float x = Input.GetAxis("Horizontal") / sensivity;
        float y = Input.GetAxis("Vertical") / sensivity;            // клавиши A, D
        if (x != 0)
        {
            Vector3 newpos = transform.position + transform.TransformDirection(new Vector3(x, 0, 0));  //из локальных координат в глобальные, чтобы объект двигался в правильном направлении.

            if (ControlDistance(Vector3.Distance(newpos, targetPos.position))) //проверяет расстояние между,является ли расстояние между позициями допустимым
                transform.position = newpos;
        }

        if (y != 0)
        {
            Vector3 newpos = transform.position + transform.TransformDirection(new Vector3(0, y, 0));

            if (ControlDistance(Vector3.Distance(newpos, targetPos.position)))
                transform.position = newpos;
        }
        // ПРИБЛИЖЕНИЕ И УДАЛЕНИЕ КАМЕРЫ К УСТАНОВКЕ НА СЦЕНЕ ПРОКРУТКОЙ КОЛЕСА МЫШИ
        float z = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed; //получения значения прокрутки колеса мыши
        if (z != 0)
        {
            Vector3 newpos = transform.position + transform.TransformDirection(Vector3.forward * z);
            if (ControlDistance(Vector3.Distance(newpos, targetPos.position)))
                transform.position = newpos;
        }


    }
    //   ОГРАНИЧЕНИЯ ПРЕДЕЛОВ ДВИЖЕНИЯ КАМЕРЫ ПО ПОМЕЩЕНИЮ
    private bool ControlDistance(float distance)
    {
        if (distance > mindistance && distance < maxdistance) return true;
        return false;
    }


}


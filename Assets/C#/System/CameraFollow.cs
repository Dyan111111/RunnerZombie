using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target; //Transform - компонент который используется чтобы взять позицию с точки 

    public Vector3 Offset;

    public Transform Player;

    public Transform MenuPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate() // вызов команд фиксированное кол-во кадров
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Target.position.y, Target.position.z) + Offset, Time.deltaTime * 10f); // перемещние камеры вверх вниз/ Lerp - метод который линейно меняет координаты вектор3 (xyz) от а до б со скоростью t // Target - движение за целью (персонажем)

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.position.x, transform.position.y, transform.position.z) + Offset, Time.deltaTime * 20f); //перемещение камеры влево и вправо 

        // плавное перемещение камеры к цели

        //transform.LookAt(Player); //заставляет камеру смотреть в сторону игрока 
    }

    public void SetTarget(Transform target)//вызывается при тапе плей, поднимает камеру 
    {
        Target = target;

        transform.eulerAngles = new Vector3(20f, 0f, 0f);

        Offset = new Vector3(0f, 1f, 8.3f);// куда смещаем камеру 
    }

    public void SetTargetFinish()
    {
        Target = MenuPoint;

        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        Offset = new Vector3(0f, 0f, 0f);
    }

    public void SetPlayer(GameObject player)
    {
        Player = player.transform;
    }
}

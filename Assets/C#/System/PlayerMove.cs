using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 6f;

    public float jumpForce = 6f;

    public float fallMultiplier = 4f;

    public Rigidbody RB;

    public LevelManager levelManager;

    private float minSwipeDistance = 20f; // дистанция на которой срабатывает свайп 
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isSwiping = false;

    private int indexMove = 1;

    public Animator animator;

    public AudioClip JumpClip;

    public AudioClip SlideClip;

    private float[] points = new float[] { -2f, 0f, 2f };

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", RB.velocity.magnitude);

        if (levelManager.IsGameStart == false) return; // если параметр IsGameStart не тру то игрок не двигается 

        RB.velocity = new Vector3(0f, RB.velocity.y, speed * levelManager.SpeedMultiple);// RB.velocity.y - скорость по y

        UIManager.instance.UpdateDistanceText(transform.position.z);
    }
    
    private void LateUpdate()
    {
        if (levelManager.IsGameStart == false) return;

        Move(); // вызов метода

        if (Application.isMobilePlatform)
        {
            Swipe();
        }

        else
        {
            KeyboardControl();
        }

        if (RB.velocity.y < 20f)
        {
            RB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // Time.deltaTime - время потраченное на отрисовку одного кадра 
        }

        //Jump();

        //Slide();
    }

    private void Move()
    {

        // Плавное перемещение по оси X (боковое перемещение)
        transform.position = new Vector3(
            Vector3.MoveTowards(transform.position, target, 10f * Time.deltaTime).x,
            transform.position.y,
            transform.position.z  // Оставляем движение вперед через Rigidbody.velocity
        );
    }

    private void Jump()
    {
            if (IsGround() == true)
            {
                RB.velocity = new Vector3(0f, jumpForce, RB.velocity.z);

                animator.SetTrigger("Jump");// вызываем триггер джамп для перехода в анимацию прыжка из бега

                SoundManager.instance.PlayShot(JumpClip);//если нужен звук то копировать эту строчку
            }
    }

    private void Slide()
    {
        if (IsGround() == false)
        {
            RB.velocity = new Vector3(0f, -jumpForce, RB.velocity.z);
        }

            StartCoroutine("ISlide");

            SoundManager.instance.PlayShot(SlideClip);//если нужен звук то копировать эту строчку
    }

    private void KeyboardControl() //управление на компе
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (indexMove > 0)
            {
                indexMove--;
                CheckIndex();
            }
        }

        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (indexMove < 2)
            {
                indexMove++;
                CheckIndex();
            }
        }

        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }
    }

    private bool IsGround() //метод возвращает тру или фолс в зависимости от того находится ли игрок на земле 
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f)) //единоразово выпускается луч из transform.position (игрока), в направление вниз Vector3.down, в переменную hit присваивается значение объекта в который попал луч, 1f - на какую дистацию выпускается луч
        {
            if (hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }

    // код для свайпа

    private void Swipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isSwiping = true;
                    startTouchPosition = touch.position; break;
                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        currentTouchPosition = touch.position;
                        CheckSwipe();
                    }
                    break;
                case TouchPhase.Ended:
                    isSwiping = false;
                    break;
            }
        }
        // For mouse input    
        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if (isSwiping)
            {
                currentTouchPosition = Input.mousePosition;
                CheckSwipe();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
        }
    }
    private void CheckSwipe()
    {
        if (isSwiping)
        {
            float horizontalSwipeDistance = currentTouchPosition.x - startTouchPosition.x;
            float verticalSwipeDistance = currentTouchPosition.y - startTouchPosition.y;
            if (Mathf.Abs(horizontalSwipeDistance) > Mathf.Abs(verticalSwipeDistance))
            {
                //Свайп для перемещения 
                if (Mathf.Abs(horizontalSwipeDistance) > minSwipeDistance)
                {
                    if (horizontalSwipeDistance > 0)
                    {
                        if (indexMove < 2)
                        {
                            indexMove++;
                            CheckIndex();
                        }
                    }
                    else
                    {
                        if (indexMove > 0)
                        {
                            indexMove--;
                            CheckIndex();
                        }
                    }
                    isSwiping = false;
                }
            }

            else
            {
                //Свайп для прыжка 
                if (Mathf.Abs(verticalSwipeDistance) > minSwipeDistance)
                {
                    if (verticalSwipeDistance > 0)
                    {
                       Jump();
                    }

                    else
                    {
                       Slide();
                    }
                    isSwiping = false;
                }
            }
        }
    }
    public bool GetSwiping() => indexMove != 0;
    private void CheckIndex()
    {
        target = new Vector3(points[indexMove], transform.position.y, transform.position.z);  // Устанавливаем целевую позицию

        SoundManager.instance.PlayShot(SoundManager.instance.LeftRightSound);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 direction = Vector3.down * 1f;

        Gizmos.DrawRay(transform.position, direction);
    }

    private IEnumerator ISlide()
    {
        transform.localScale = new Vector3(1f, 0.5f, 1f);

        yield return new WaitForSeconds(1.5f);

        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}

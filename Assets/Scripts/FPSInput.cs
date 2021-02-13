using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class FPSInput : MonoBehaviour
{
  private CharacterController _charController;
  public float speed = 6f;
  public float gravity = -9.8f;

  void Start()
  {
    // компонент для управления персонажем и определяет столкновения
    _charController = GetComponent<CharacterController>();
  }

  void Update()
  {
    // вычисляем нажатия клавиш A/D W/S
    float deltaX = Input.GetAxis("Horizontal") * speed;
    float deltaZ = Input.GetAxis("Vertical") * speed;
    Vector3 movement = new Vector3(deltaX, 0, deltaZ);

    // ограничение скорости перемещения по вертикали
    // необходимо иначе перс будет двигаться быстрее
    movement = Vector3.ClampMagnitude(movement, speed) * Time.deltaTime;
    // Переносит локальные координаты в глобальные
    movement = transform.TransformDirection(movement);
    movement.y = gravity;

    _charController.Move(movement);

    // transform.Translate(deltaX, 0, deltaZ);
  }
}

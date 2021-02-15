using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WanderingAI))]
public class ReactiveTarget : MonoBehaviour
{
  private WanderingAI _enemy;

  void Start()
  {
    _enemy = GetComponent<WanderingAI>();
  }

  // нанесение урона врагу
  public void ReactToHit()
  {
    if (!_enemy.getAlive()) return;
    _enemy.SetAlive(false);
    // компонент нужен для падения
    gameObject.AddComponent<Rigidbody>();
    StartCoroutine(Die());
  }

  // метод убийства врага
  private IEnumerator Die()
  {
    transform.Rotate(-75, 0, 0);

    yield return new WaitForSeconds(1.75f);

    Destroy(gameObject);
  }
}

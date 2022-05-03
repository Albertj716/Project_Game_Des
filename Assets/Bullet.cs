using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

  void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    
  IEnumerator SelfDestruct()
  {
      yield return new WaitForSeconds(2f);
      Destroy(gameObject);
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
      Destroy(gameObject);
  }

}

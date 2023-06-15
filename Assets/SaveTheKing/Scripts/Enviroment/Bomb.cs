using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float time = 5;
    [SerializeField] private Animation _animation;
    [SerializeField] private GameObject ExplVFX;
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(time);
        _animation.Play("Explosion");
        Instantiate(ExplVFX, transform.position,Quaternion.identity);
    }

    private void ExplosionStarter()
    {
        StartCoroutine(Explosion());
    }

    private void Start()
    {
        GameSceneManager.instanse.onGameStart += ExplosionStarter;
    }

   

    private void OnDisable() => GameSceneManager.instanse.onGameStart -= ExplosionStarter;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Pet"))
            col.gameObject.GetComponent<Pet>().Lose();
        if(col.gameObject.CompareTag("Enemy"))
            col.gameObject.GetComponent<Enemy>().Death();
                //if(col.gameObject.CompareTag("Land"))
            //(col.gameObject.GetComponent<Land>().isDestoyed)
             //   col.gameObject.GetComponent<Land>().SelfDestroy();
    }
}

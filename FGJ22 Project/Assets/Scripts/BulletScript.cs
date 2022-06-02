using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damageDealt = 10;
    public float knockbackForce = -2f;
    public float pullForce = 10f;
    public float forceTime = 3f;
    public float destroyBulletTime = 5;

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject != null)
        {
            Destroy(gameObject, destroyBulletTime);
        }
    }

    // Push enemy if colliding with enemy object
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.GetComponent<EnemyScriptPos>().notStuck)
        {
            GetComponent<SphereCollider>().isTrigger = false;
            GameObject bulletGraphics = transform.Find("BulletGFX").gameObject;
            bulletGraphics.SetActive(false);
            other.gameObject.GetComponent<EnemyScriptPos>().speed = knockbackForce;           
            yield return new WaitForSeconds(forceTime);
            //other.gameObject.GetComponent<EnemyScriptPos>().speed = 3f;      
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemy2" && other.GetComponent<EnemyScriptNeg>().notStuck)
        {
            GetComponent<SphereCollider>().isTrigger = false;
            GameObject bulletGraphics = transform.Find("BulletGFX").gameObject;
            bulletGraphics.SetActive(false);
            other.gameObject.GetComponent<EnemyScriptNeg>().speed = pullForce;
            yield return new WaitForSeconds(forceTime);
            //other.gameObject.GetComponent<EnemyScriptPos>().speed = 3f;      
            Destroy(gameObject);
        }
    }

    
}

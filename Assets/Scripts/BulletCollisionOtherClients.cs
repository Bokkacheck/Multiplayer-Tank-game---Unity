using UnityEngine;

public class BulletCollisionOtherClients : MonoBehaviour
{
    private float brojac = 0;
    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Ostali");
        //Ovde treba ubaciti neki efekat kasnije !!
        Destroy(gameObject);
    }
    void Update()
    {
        brojac += Time.deltaTime;
        if (brojac > 2.0f)
        {
            Destroy(gameObject);
        }
    }
}

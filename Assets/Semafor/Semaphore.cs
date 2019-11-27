using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaphore : MonoBehaviour
{
    [SerializeField]
    private Light zeleno;
    [SerializeField]
    private Light zuto;
    [SerializeField]
    private Light crveno;
    private int brojac = 0;
    // Start is called before the first frame update
    void Start()
    {
        crveno.enabled = zeleno.enabled = zuto.enabled = false;
        StartCoroutine(Semafor());
    }
    IEnumerator Semafor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            brojac++;
            if (brojac < 5)
            {
                zeleno.enabled = true;
                zuto.enabled = false;
            }
            else if(brojac<6){
                zuto.enabled = true;
            }
            else if (brojac < 7)
            {
                zeleno.enabled = false;
            }
            else if (brojac < 8)
            {
                zuto.enabled = false;
                crveno.enabled = true;
            }
            else if (brojac < 11)
            {
                crveno.enabled = true;
            }
            else if (brojac < 12)
            {
                crveno.enabled = false;
                zuto.enabled = true;
            }
            else if(brojac<13)
            {
                brojac = 0;
            }
        }
    }
}

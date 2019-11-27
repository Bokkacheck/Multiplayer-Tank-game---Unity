using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Move : NetworkBehaviour
{
    [SerializeField]
    private float speed = 6.0f;
    private float currentForce = 0.0f;
    private float maxForce = 200f;  
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private GameObject turret;
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject vrh;
    [SerializeField]
    private GameObject[] bullets;
    [SerializeField]
    private Texture2D image;
    private RaycastHit hitBefore;
    private float gunRotation = 0;
    private int accelerationSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -75, 0);
        hitBefore = new RaycastHit();
    }

    //PUCANJE multiplayer
    [Command]
    void CmdShoot(int mod,string playerWhoShooted)
    {
        RpcEveryOneShoot(mod,playerWhoShooted);
    }
    [ClientRpc]
    void RpcEveryOneShoot(int mod,string playerWhoShooted)
    {
        GameObject bullet = Instantiate(bullets[mod], vrh.transform.position, vrh.transform.rotation);
        bullet.transform.Rotate(90, 0, 0);
        bullet.GetComponent<Weapon>().playerWhoShooted = playerWhoShooted;
        bullet.GetComponent<Rigidbody>().AddForce(gun.transform.forward * 500, ForceMode.Impulse);
        if (isLocalPlayer)
        {
            bullet.AddComponent<BulletCollisionClientWhoShooted>();
        }
        else
        {
            bullet.AddComponent<BulletCollisionOtherClients>();
        }
    }
    //Kraj pucanje MULTIPLAYER
    void FixedUpdate()
    {
        float rotateX =Input.GetAxis("Horizontal");
        float moveForward = Input.GetAxis("Vertical");
        if (moveForward != 0)
        {
            rb.velocity = transform.forward * moveForward * speed;
        }
        if (moveForward >= 0)
        {
            transform.Rotate(0, rotateX, 0);
        }
        else
        {
            transform.Rotate(0, -rotateX, 0);
        }
        //PUCANJE
        if (Input.GetKey(KeyCode.Space))
        {
            CmdShoot(0,transform.name);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdShoot(1,transform.name);
        }
        //Kraj pucanja
        float rotateTurret = Input.GetAxis("Mouse X");
        float rotateGun = Input.GetAxis("Mouse Y");
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Camera.main.transform.RotateAround(turret.transform.position, turret.transform.up, rotateTurret);
            Camera.main.transform.RotateAround(turret.transform.position, Camera.main.transform.right, -rotateGun / 2);
        }
        else
        {
            Camera.main.transform.RotateAround(turret.transform.position, Camera.main.transform.right, -rotateGun / 2);
            turret.transform.Rotate(0, rotateTurret, 0);
            gunRotation += rotateGun;
            if (gunRotation < 40 && gunRotation > -15)
            {
                gun.transform.Rotate(-rotateGun, 0, 0);
            }
        }
    }

    //NISAN
    void OnGUI()
    {
        RaycastHit hit;
        if (Physics.Raycast(vrh.transform.position, gun.transform.forward, out hit, 1000))
        {
            if(hit.collider.tag == "Bullet")
            {
                hit = hitBefore;
            }
        }
        else
        {
            hit.point = vrh.transform.forward*int.MaxValue;
        }
        hitBefore = hit; 
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.point);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.DrawTexture(new Rect(screenPosition.x - 70f / 2, screenPosition.y - 70f / 2.0f, 70, 70), image);
    }
}

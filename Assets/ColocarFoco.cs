using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarFoco : MonoBehaviour
{
    public GameObject foco;
    public GameObject focoEncendido;
    public GameObject boquilla;
    public girofaro giro;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entra: " + other.ToString() + " instancia: " + other.GetInstanceID().ToString());
        if (other.gameObject.tag == "foco")
        {
            //Instantiate(foco.gameObject, boquilla.position-new Vector3(1f,0f,0f), boquilla.rotation);
            //Debug.Log("Crea");
            Destroy(other.gameObject);
            Debug.Log("Destuye");
            focoEncendido.SetActive(true);
            giro.TieneFoco = true;
        }

        //GameEvents.current.colocarFoco(id);
        //GameEvents.current.DoorwayTriggerEnter(id);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarFoco : MonoBehaviour
{
    public GameObject foco;
    public GameObject focoEncendido;

    [SerializeField] int cantidadDeFocos = 3;
    [SerializeField,Range(1,10)] float tiempoDelFoco = 2;
    [SerializeField,Range(0,5)] float tiempoDelFocoVariation = 2;
    [SerializeField] DialogEvent focoPuesto = null;
    [SerializeField] DialogEvent focoQuemado = null;
    [SerializeField] DialogEvent focosCompletos = null;
    public girofaro giro;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entra: " + other.ToString() + " instancia: " + other.GetInstanceID().ToString());
        if (other.gameObject.tag == "foco")
        {
            //Instantiate(foco.gameObject, boquilla.position-new Vector3(1f,0f,0f), boquilla.rotation);
            //Debug.Log("Crea");
            Destroy(other.gameObject);
            // Debug.Log("Destuye");
            focoEncendido.SetActive(true);
            giro.TieneFoco = true;
            cantidadDeFocos--;
            if(cantidadDeFocos<=0){
                focosCompletos.TriggerEvent();
            }
            else{
                Debug.Log("Foco puesto");
                focoPuesto.TriggerEvent();
                Invoke("FocoQuemado",tiempoDelFoco + Random.Range(-tiempoDelFocoVariation,tiempoDelFocoVariation));
            }
        }

        //GameEvents.current.colocarFoco(id);
        //GameEvents.current.DoorwayTriggerEnter(id);

    }
    void FocoQuemado(){
            focoQuemado.TriggerEvent();
            giro.TieneFoco = false;
            focoEncendido.SetActive(false);
    }
}

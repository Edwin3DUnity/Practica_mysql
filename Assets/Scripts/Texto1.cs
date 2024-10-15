using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Texto1 : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "Hola, " + GestionBD.singleton.nombreUsuario + ", Bienvenido.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeScores(int nScore)
    {
        GestionBD.singleton.Score_Actualizar(nScore);
    }
}

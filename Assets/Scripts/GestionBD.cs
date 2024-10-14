using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionBD : MonoBehaviour
{
    public InputField txtUsuario;
    public InputField txtContraseña;
    
    
    
    
    
    //Metodos de llamado
    public void iniciarSesion()
    {
        StartCoroutine(Login());
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Login()
    {
        WWW coneccion = new WWW("http://localhost/pract/login.php?uss="+ txtUsuario.text + "&pss="+ txtContraseña.text);
        yield return(coneccion);
     //   Debug.Log(coneccion.text);
     if (coneccion.text == "200")
     {
         print("El Usuario si existe");
     }else if (coneccion.text == "401")
     {
         print("Usuario o contraseña incorrectos");
     }
     else
     {
         print("Error en la coneccion");
     }
    }
    
    
    
}

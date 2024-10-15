using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionBD : MonoBehaviour
{
    public InputField txtUsuario;
    public InputField txtContraseña;
    public string     nombreUsuario;
    public int        scoreUsuario;
    public bool     sesionIniciada = false;
    
    ///  Respuestas WEB
    ///         400  -  No pudo establecer conexion;
    ///         401  -  No encontró datos
    ///         402  -  eL usuario ya existe
    /// 
    ///
    ///
    ///          200  - Datos encontrados
    ///          201  - Usuario registrado
    ///
    ///
    ///
    ///
    /// 
    
    
    //Metodos de llamado
    public void iniciarSesion()
    {
        StartCoroutine(Login());
        StartCoroutine(Datos());
    }
    
    public void RegistrarUsuario()
    {
        StartCoroutine(Registrar());
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
    IEnumerator Datos()
    {
        WWW coneccion = new WWW("http://localhost/pract/datos.php?uss="+ txtUsuario.text );
        yield return(coneccion);
        //   Debug.Log(coneccion.text);
         if (coneccion.text == "401")
        {
            print("Usuario incorrectos");
        }
         else
         {
             string[] nDdatos = coneccion.text.Split('|');
             if (nDdatos.Length != 3)
             {
                 print("Error en la coneccion ndatos");
             }
             else
             {
                 nombreUsuario = nDdatos[0];
                 scoreUsuario = int.Parse(nDdatos[1]);
                 sesionIniciada = true;
             }
         }
      
    }
    
    IEnumerator Registrar()
    {
        WWW coneccion = new WWW("http://localhost/pract/registro.php?uss="+ txtUsuario.text + "&pss=" + txtContraseña.text);
        yield return(coneccion);
        //   Debug.Log(coneccion.text);
        if (coneccion.text == "402")
        {
            Debug.LogError("Usuario ya existe");
        }
        else if(coneccion.text == "201")
        {

            nombreUsuario = txtUsuario.text;
            scoreUsuario = 0;
            sesionIniciada = true;

        }
        else
        {
            Debug.LogError("Error en la coneccion con la base de datos al intentar registrar");
        }
      
    }
    
}

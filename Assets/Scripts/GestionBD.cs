using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestionBD : MonoBehaviour
{
    public InputField txtUsuario;
    public InputField txtContraseña;
    public string     nombreUsuario;
    public int        scoreUsuario;
    public int         idUsuario;
    public bool     sesionIniciada = false;

    public static GestionBD singleton;
    
    ///  Respuestas WEB
    ///         400  -  No pudo establecer conexion;
    ///         401  -  No encontró datos
    ///         402  -  eL usuario ya existe
    /// 
    ///
    ///
    ///          200  - Datos encontrados
    ///          201  - Usuario registrado
    ///          202  - Score Actualizado
    ///
    ///
    ///
    /// 
    
    
    //Metodos de llamado
    public void iniciarSesion()
    {
        StartCoroutine(Login());
        
    }
    
    public void RegistrarUsuario()
    {
        StartCoroutine(Registrar());
    }

    public void Score_Actualizar(int nScore)
    {
        StartCoroutine(ActualizarScore( nScore));
    }

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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
         StartCoroutine(Datos());
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
                 idUsuario = int.Parse(nDdatos[2]);
                 sesionIniciada = true;
                 SceneManager.LoadScene("Inicio juego");
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
    
    IEnumerator ActualizarScore(int nScore)
    {
        WWW coneccion = new WWW("http://localhost/pract/score.php?uss="+ txtUsuario.text + "&nScore=" + nScore.ToString());
        yield return(coneccion);
        //   Debug.Log(coneccion.text);
        if (coneccion.text == "1")
        {
            Debug.LogError("Usuario no existe");
        }
        else if(coneccion.text == "202")
        {
            print("Datos actualizados correctamente");
            scoreUsuario = nScore;
        }
        else
        {
            Debug.LogError("Error en la coneccion con la base de datos al intentar actualizar el score");
        }
      
    }

    
}

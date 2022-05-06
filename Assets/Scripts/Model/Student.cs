using System.Collections.Generic;

[System.Serializable]
public class Dato
{
    public int Id;
    public string nombre;
    public string apellido;
    public string email;
    public float nota;
    public int edad;
}

[System.Serializable]
public class Root
{
    public List<Dato> datos;
}


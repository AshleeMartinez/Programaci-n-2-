using System;


namespace CRUD_CloudWaves
{
    public class Producto
    {
        private string marca;
        private string sabor;
        private int cantidad;
        private int precio;
        private string persona;
        private int iD;

        public string Marca { get => marca; set => marca = value; }
        public string Sabor { get => sabor; set => sabor = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public int Precio { get => precio; set => precio = value; }
        public string Persona { get => persona; set => persona = value; }
        public int ID { get => iD; set => iD = value; }

    }
}

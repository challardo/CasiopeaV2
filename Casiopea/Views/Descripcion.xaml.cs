using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Casiopea.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Descripcion : ContentPage
    {
        public Descripcion(int ProductoId, string Nombre, string Precio, string Descripcion, string Imagen, string Categoria )
        {
            InitializeComponent();
            ID.Text = "Product Id: " + ProductoId.ToString();
            NOMBRE.Text = "Product Name: " + Nombre;
            PRECIO.Text = "Product Category: " + Precio;
            DESCRIPCION.Text = "Product Description: " + Descripcion;
            CATEGORIA.Text = Categoria;
            IMAGEN.Source = Imagen;
            Test.Text = Imagen.ToString();
        }
    }
}
using Casiopea.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Casiopea.Services
{
    class FirebaseStorageHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://casiopea-7c33b.firebaseio.com/");

        public async Task<List<Producto>> GetAllProducts()
        {

            return (await firebase
              .Child("Products")
              .OnceAsync<Producto>()).Select(item => new Producto
              {
                  ProductoId = item.Object.ProductoId,
                  Nombre = item.Object.Nombre,
                  Precio = item.Object.Precio,
                  Descripcion = item.Object.Descripcion,
                  Imagen = item.Object.Imagen,
                  Categoria= item.Object.Categoria
              }).ToList();
        }

        public async Task AddProducto(int productoid, string nombre, string precio, string descripcion, string imagen, string categoria)
        {

            await firebase
              .Child("Products")
              .PostAsync(new Producto() { ProductoId = productoid, Nombre = nombre, Precio = precio, Descripcion = descripcion, Imagen = imagen, Categoria = categoria });
        }

        public async Task<Producto> GetProducto(int productid)
        {
            var allProducts = await GetAllProducts();
            await firebase
              .Child("Products")
              .OnceAsync<Producto>();
            return allProducts.Where(a => a.ProductoId == productid).FirstOrDefault();
        }
        public async Task<Producto> GetProductoPorImg(String Imagen)
        {
            var allproducts = await GetAllProducts();
            await firebase
                .Child("Products")
                .OnceAsync<Producto>();
            return allproducts.Where(a => a.Imagen == Imagen).FirstOrDefault();
        }


        public async Task<List<Producto>> GetProductoPorCategoria(String cat)
        {
            var allproducts = await GetAllProducts();
            await firebase
                .Child("Products")
                .OnceAsync<Producto>();
            return allproducts.Where(a => a.Categoria == cat).ToList();

        }

    }
}

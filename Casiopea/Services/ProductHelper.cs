using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Casiopea.Models;
using System.Linq;

namespace Casiopea.Services
{
    class ProductHelper
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
                  Imagen = item.Object.Imagen

              }).ToList();
        }

        public async Task<Producto> GetProducto(int productid)
        {
            var allProducts = await GetAllProducts();
            await firebase
              .Child("Products")
              .OnceAsync<Producto>();
            return allProducts.Where(a => a.ProductoId == productid).FirstOrDefault();
        }


    }
}

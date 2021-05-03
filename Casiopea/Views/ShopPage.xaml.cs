using Casiopea.Models;
using Casiopea.Services;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Casiopea.Services;
using System.Windows.Input;
using Xamarin.Essentials;
namespace Casiopea.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopPage : ContentPage
    {
        FirebaseClient firebase = new FirebaseClient("https://casiopea-7c33b.firebaseio.com/");
       // ProductHelper PH = new ProductHelper();
        FirebaseStorageHelper PH = new FirebaseStorageHelper();
        public ShopPage()
        {
            InitializeComponent();

            var current1 = Connectivity.NetworkAccess;

            if (current1 == NetworkAccess.Internet)
            {
                CargarTodo();
            }
            else
            {
                DisplayAlert("Bad conection", "please check connection", "OK");
               
            }

            ICommand refreshCommand = new Command(() =>
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    refreshView.IsRefreshing = true;
                    CargarTodo();

                    refreshView.IsRefreshing = false;
                }
                else
                {
                    DisplayAlert("Bad conection", "please check connection", "OK");
                    refreshView.IsRefreshing = false;
                }

            });
            refreshView.Command = refreshCommand;
            this.BindingContext = this;

        }

      
        public async void CargarTodo()
        {

            var allProducts = await PH.GetProductoPorCategoria("BestSeller");
            lstProductos.ItemsSource = allProducts;
            var allProducts2 = await PH.GetProductoPorCategoria("Gala");
            lstProductos2.ItemsSource = allProducts2;
            // var allProducts = await PH.GetAllProducts();
            // lstProductos.ItemsSource = allProducts;

        }
        public async void CargarProducto(int id)
        {

            int ProductoId; string Nombre; string Precio; string Descripcion; string Imagen; string Categoria;
            // var producto = await PH.GetProductoPorCategoria("BestSeller");
            var producto = await PH.GetProducto(id);
            if (producto != null)
            {
                ProductoId = producto.ProductoId;
                Nombre = producto.Nombre;
                Precio = producto.Precio;
                Descripcion = producto.Descripcion;
                Imagen = producto.Imagen;
                Categoria = producto.Categoria;
                await Navigation.PushAsync(new Descripcion(producto.ProductoId, producto.Nombre, producto.Precio, producto.Descripcion, producto.Imagen, producto.Categoria));
                // await DisplayAlert("Success", "Product Retrive Successfully", "OK");
            }
            else
            {
                await DisplayAlert("Success", "No Product Available", "OK");
            }
        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {


            var mydetails = e.Item as Producto;

            await Navigation.PushAsync(new Descripcion(mydetails.ProductoId, mydetails.Nombre, mydetails.Precio, mydetails.Descripcion, mydetails.Imagen, mydetails.Categoria));
        }
    

        private void lstProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // int previous =Convert.ToInt32( (e.PreviousSelection.FirstOrDefault() as Producto)?.ProductoId);
            int current = Convert.ToInt32((e.CurrentSelection.FirstOrDefault() as Producto)?.ProductoId);
            CargarProducto(current);
        }





        // ESTO SE VA A BORRAR 
        private Timer timer;
        public List<Banner> Banners { get => GetBanners(); }
        public List<Product> CollectionsList { get => GetCollections(); }
        public List<Product> TrendsList { get => GetTrends(); }

        public async void OnProductClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductView());
        }
        private List<Banner> GetBanners()
        {
            var bannerList = new List<Banner>();
            bannerList.Add(new Banner { Heading = "SUMMER COLLECTION", Message = "40% Discount", Caption = "BEST DISCOUNT THIS SEASON", Image = "classic.png" });
            bannerList.Add(new Banner { Heading = "WOMEN'S CLOTHINGS", Message = "UP TO 50% OFF", Caption = "GET 50% OFF ON EVERY ITEM", Image = "womenCol.png" });
            bannerList.Add(new Banner { Heading = "ELEGANT COLLECTION", Message = "20% Discount", Caption = "UNIQUE COMBINATIONS OF ITEMS", Image = "elegantCol.png" });
            return bannerList;
        }

        private List<Product> GetCollections()
        {
            var trendList = new List<Product>();
            trendList.Add(new Product { Image = "floral.png", Name = "Floral Bag + Hat", Price = "$123.50" });
            trendList.Add(new Product { Image = "satchel.png", Name = "Satchel Bag", Price = "$49.99" });
            trendList.Add(new Product { Image = "leatherBag.png", Name = "Leather Bag", Price = "$40.99" });
            return trendList;
        }

        private List<Product> GetTrends()
        {
            var colList = new List<Product>();
            colList.Add(new Product { Image = "heeledShoe.png", Name = "Beige Heeled Shoe", Price = "$109.99" });
            colList.Add(new Product { Image = "dressShoe.png", Name = "Shoe + Addons", Price = "$225.99" });
            return colList;
        }

        protected override void OnAppearing()
        {
            timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds) { AutoReset = true, Enabled = true };
            timer.Elapsed += Timer_Elapsed;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            timer?.Dispose();
            base.OnDisappearing();
        }
      
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                if (cvBanners.Position == 2)
                {
                    cvBanners.Position = 0;
                    return;
                }

                cvBanners.Position += 1;
            });
        }
    }
 

    public class Banner
    {
        public string Heading { get; set; }
        public string Message { get; set; }
        public string Caption { get; set; }
        public string Image { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
    }


}
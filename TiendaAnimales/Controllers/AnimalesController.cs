using AnimalesMVC.DAL;
using AnimalesMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TiendaAnimales.Models;
using TuProyecto.DAL;

namespace TiendaAnimales.Controllers
{
    public class AnimalesController : Controller
    {
        private readonly AnimalDAL animalDAL;
        private readonly TipoAnimalDAL tipoAnimalDAL;

        public AnimalesController()
        {
            animalDAL = new AnimalDAL(Conexion.CadenaBBDD);
            tipoAnimalDAL = new TipoAnimalDAL(Conexion.CadenaBBDD);
        }
        public IActionResult Index()
        {
            ViewBag.Mensaje = "¡Valor pasado por viewbag!";

            List<Animal> lstAnimals = new List<Animal>();
            //{
            //    new Animal() {IdAnimal = 1, NombreAnimal = "Gato"},
            //    new Animal() {IdAnimal = 2, NombreAnimal = "Perro"},
            //    new Animal() {IdAnimal = 3, NombreAnimal = "Conejo"},
            //    new Animal() {IdAnimal = 4, NombreAnimal = "Hamster"},
            //};

            lstAnimals = animalDAL.ObtenerTodosLosAnimales();
            var tiposAnimales = tipoAnimalDAL.ObtenerTodosLosTiposAnimales();
            Random rnd = new Random();
            int numeroAleatorio = rnd.Next(0, tiposAnimales.Count);

            ViewBag.EjemploBlabla = "El " + tiposAnimales[numeroAleatorio].TipoDescripcion + " es tu animal de la suerte!";

            return View(lstAnimals);
        }

        public IActionResult Crear()
        {
            CargarComboTipoAnimal();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Animal nuevoAnimal)
        {
            if (ModelState.IsValid)
            {
                animalDAL.InsertarAnimal(nuevoAnimal);
                return RedirectToAction("Index"); // Redirigir a la acción "Index"
            }
            CargarComboTipoAnimal();


            return View(nuevoAnimal);
        }        


        private void CargarComboTipoAnimal()
        {
            var tiposAnimales = tipoAnimalDAL.ObtenerTodosLosTiposAnimales();
            // Agregar la opción predeterminada "Seleccionar..."
            tiposAnimales.Insert(0, new TipoAnimal { IdTipoAnimal = 0, TipoDescripcion = "Seleccionar..." });

            ViewBag.TiposAnimales = new SelectList(tiposAnimales, "IdTipoAnimal", "TipoDescripcion");
        }
    }
}

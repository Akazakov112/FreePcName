using FreePcNameWeb.Models;
using FreePcNameWeb.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FreePcNameWeb.Controllers
{
    /// <summary>
    /// Контроллер главного окна.
    /// </summary>
    public class MainController : Controller
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private OspsContext OspsDb { get; }

        /// <summary>
        /// Поисковик свободных имен.
        /// </summary>
        private IFreenameSearcher FreenamesSearcher { get; }

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="database">Контекст базы данных</param>
        public MainController(OspsContext database, IFreenameSearcher searcher)
        {
            OspsDb = database;
            FreenamesSearcher = searcher;
        }


        /// <summary>
        /// Действие Index для запросов Get.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Osps> osps = OspsDb.Osps
                .Where(x => !string.IsNullOrWhiteSpace(x.ShortName) && x.ShortName != "NULL" && x.ShortName != null
                        && x.City != null && !string.IsNullOrWhiteSpace(x.City)
                        && x.Address != null && !string.IsNullOrWhiteSpace(x.Address))
                .OrderBy(x => x.Osp)
                .ToList()
                .Distinct(new OspsComparer());

            return View(osps);
        }

        /// <summary>
        /// Действие Index для запросов Post.
        /// </summary>
        /// <param name="selectOsp"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(string selectOsp)
        {
            Freenames fn = FreenamesSearcher.GetFreenames(selectOsp);
            return Json(fn);
        }
    }
}

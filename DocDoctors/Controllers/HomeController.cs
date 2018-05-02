using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocDoctors.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocDoctors.DAL;

namespace DocDoctors.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["CompanyName"] = "Document Doctors Ltd.";

            //List<Client> clients = new List<Client>();
            //clients.Add(new Client() { ClientId = 1, Name = "Glenn" });
            //clients.Add(new Client() { ClientId = 2, Name = "Dan" });
            ClientGateway clientGateway = new ClientGateway();
            ClientCollection clients = clientGateway.GetClients();

            IEnumerable<SelectListItem> items = new List<SelectListItem>();
            items = clients.Select(c => new SelectListItem
            {
                Value = c.ClientId.ToString(),
                Text = c.Name
            });

            //List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "MyId1", Value = "MyId1", Selected = true });
            //items.Add(new SelectListItem { Text = "MyId2", Value = "MyId2" });
            //items.Add(new SelectListItem { Text = "MyId3", Value = "MyId3" });
            ViewBag.ListOfClients = items;

            return View();
        }

        public IActionResult Client()
        {
            ClientGateway clientGateway = new ClientGateway();
            ClientCollection clients = clientGateway.GetClients();
            return View("Client", clients);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

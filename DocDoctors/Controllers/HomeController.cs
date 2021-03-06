﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DocDoctors.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocDoctors.DAL;
using System.Net.Http;
using System.Net;

namespace DocDoctors.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["CompanyName"] = "Document Doctors Ltd.";

            ClientGateway clientGateway = new ClientGateway();
            ClientCollection clients = clientGateway.GetClients();
            return View("Index", clients);
        }

        public async Task<IActionResult> Sent()
        {
            var values = new Dictionary<string, string>
            {
               { "Name", "TEst" }
            };

            var content = new FormUrlEncodedContent(values);
            HttpClient client = new HttpClient();
            var result = await client.PostAsync("http://localhost:38661/api/values", content);
            return View();
        }

        public IActionResult Edit(int id, string name)
        {
            FileRepoCollection repos = new FileRepoCollection();
            repos.Add(new FileRepo() { Id = 1, Name = "Dropbox" });
            repos.Add(new FileRepo() { Id = 2, Name = "Google Drive" });
            repos.Add(new FileRepo() { Id = 3, Name = "One Drive" });

            IEnumerable<SelectListItem> items = new List<SelectListItem>();
            items = repos.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });

            FileRepo repo = new FileRepo() { Id = 1, Name = "Google Drive" };
            Client client = new Models.Client() { ClientId = id, Name = name, FileRepo=repo };
            ViewBag.ListOfRepos = items;
            return View("Edit", client);
        }

        public IActionResult Send()
        {
            string result = string.Empty;
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("http://localhost:38661/api/values");
                HttpResponseMessage response = client.GetAsync("").Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
            }

            List<Document> documents = new List<Document>();
            string[] files = result.Split(",");
            foreach (string file in files)
            {
                documents.Add(new Document() { Name = file.Replace("]","").Replace("[","").Replace("\"", "") });
            }

            //documents.Add(new Document() { Name = "Glenn's Tax Return.docx" });
            //documents.Add(new Document() { Name = "Year end results.pdf" });

            IEnumerable<SelectListItem> items = new List<SelectListItem>();
            items = documents.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });
            ViewBag.ListOfDocuments = items;

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

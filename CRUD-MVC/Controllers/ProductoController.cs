﻿using CRUD_MVC.Models;
using CRUD_MVC.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace CRUD_MVC.Controllers
{
    public class ProductoController : Controller
    {
        private readonly HttpClient _clienteHTTP;
        private readonly string _apiBaseUrl = "http://localhost:5078/";
        public ProductoController()
        {
            _clienteHTTP = new HttpClient()
            {
                BaseAddress = new Uri(_apiBaseUrl)
            };
        }

        // GET: ProductoController
        public async Task<IActionResult> Index()
        {
            var productos = await _clienteHTTP.GetFromJsonAsync<List<Producto>>("api/Producto");
            return View(productos);
        }

        // GET: ProductoController/Details/5
        public async Task<IActionResult> Details(int IdProducto)
        {
            var producto = await _clienteHTTP.GetFromJsonAsync<Producto>($"api/Producto/{IdProducto}");
            if (producto != null) return View(producto);
            return RedirectToAction("Index");
        }

        // GET: ProductoController/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            await _clienteHTTP.PostAsJsonAsync("api/Producto", producto);
            return RedirectToAction("Index");
        }

        // GET: ProductoController/Edit/5
        public async Task<IActionResult> Edit(int IdProducto)
        {
            var producto = await _clienteHTTP.GetFromJsonAsync<Producto>($"api/Producto/{IdProducto}");
            if (producto != null) return View(producto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto nuevo)
        {
            await _clienteHTTP.PutAsJsonAsync($"api/Producto/{nuevo.IdProducto}", nuevo);

            return RedirectToAction("Index");
        }

        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(int IdProducto)
        {
            await _clienteHTTP.DeleteAsync($"api/Producto/{IdProducto}");

            return RedirectToAction("Index");
        }
    }
}

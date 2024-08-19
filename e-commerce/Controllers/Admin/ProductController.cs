using e_commerce.Models;
using e_commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers.Admin
{
    [Route("admin/[controller]")]
    public class ProductController : Controller
    {

        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var cats = await _productService.GetAllAsync();

            return View("~/Views/Admin/Product/Index.cshtml", cats);
        }


        [HttpGet]
        [Route("CreateProduct")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Product/Create.cshtml");
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Admin/Product/Create.cshtml", product);
        }

        //update Product
        [HttpGet]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            Product ProductFromDB = await _productService.GetByIdAsync(id.Value);
            if (ProductFromDB == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Product/Edit.cshtml", ProductFromDB);
        }
        [HttpPost]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Admin/Product/Edit.cshtml", product);
        }

        //delete Product
        [HttpGet]
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            Product productFromDB = await _productService.GetByIdAsync(id.Value);
            if (productFromDB == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Product/delete.cshtml", productFromDB);
        }


        [HttpPost]
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeletePOST(int? id)

        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            Product? productFromDB = null;
            try
            {
                productFromDB = await _productService.GetByIdAsync(id.Value);
                if (productFromDB == null)
                {
                    return NotFound(nameof(Product));
                }

                await _productService.DeleteAsync(id.Value);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["ErrorMsg"] = ex.Message;
                return View("~/Views/Admin/Product/delete.cshtml", productFromDB);
            }
        }
    }
}

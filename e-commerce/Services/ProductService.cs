using e_commerce.Base;
using e_commerce.Data;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Services
{
    public class ProductService : BaseService<Product>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            if (product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                product.ImagePath = "/images/" + uniqueFileName;
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                // Delete old image if exists
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                product.ImagePath = "/images/" + uniqueFileName;
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
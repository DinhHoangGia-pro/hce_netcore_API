using Microsoft.AspNetCore.Mvc;
using demoAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace demoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        [HttpGet("ListProducts")]
        public List<Product> ListProducts()
        {
            ProductDBContext db = new ProductDBContext();
            return db.Products.ToList();
        }

        [HttpGet("ListShippers")]
        public List<Shipper> ListShippers()
        {
            ProductDBContext db = new ProductDBContext();
            return (from moiphantu in db.Shippers select moiphantu).ToList();
        }

        /* Danh mục Categories  */
        [HttpGet("ListCategories")]
        public IEnumerable<Category> ListCategories()
        {
            ProductDBContext dtx = new ProductDBContext();
            return dtx.Categories;
        }

        /* Danh mục Supplier  */
        [HttpGet("ListSuppliers")]
        public IEnumerable<Supplier> ListSuppliers()
        {
            ProductDBContext dtx = new ProductDBContext();
            return dtx.Suppliers;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            ProductDBContext dbx = new ProductDBContext();
            Product product = (from x in dbx.Products where x.ProductID == id select x).FirstOrDefault();
            return product;
        }


        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                ProductDBContext dtx = new ProductDBContext();
                // Validate foreign keys
                var categoryExists = await dtx.Categories.AnyAsync(c => c.CategoryID == product.CategoryID);
                var supplierExists = await dtx.Suppliers.AnyAsync(s => s.SupplierID == product.SupplierID);

                if (!categoryExists || !supplierExists)
                {
                    return BadRequest("Invalid CategoryID or SupplierID");
                }

                // Ensure required fields
                if (string.IsNullOrEmpty(product.ProductName))
                {
                    return BadRequest("Product name is required");
                }

                // Add product
                await dtx.Products.AddAsync(product);
                await dtx.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = product.ProductID }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: Update existing product
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                ProductDBContext dtx = new ProductDBContext();
                var existingProduct = await dtx.Products.FindAsync(id);
                if (existingProduct == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }

                // Validate foreign keys
                var categoryExists = await dtx.Categories.AnyAsync(c => c.CategoryID == product.CategoryID);
                var supplierExists = await dtx.Suppliers.AnyAsync(s => s.SupplierID == product.SupplierID);

                if (!categoryExists || !supplierExists)
                {
                    return BadRequest("Invalid CategoryID or SupplierID");
                }

                // Update properties
                existingProduct.ProductName = product.ProductName;
                existingProduct.CategoryID = product.CategoryID;
                existingProduct.SupplierID = product.SupplierID;
                existingProduct.QuantityPerUnit = product.QuantityPerUnit;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.UnitsInStock = product.UnitsInStock;
                existingProduct.UnitsOnOrder = product.UnitsOnOrder;
                existingProduct.ReorderLevel = product.ReorderLevel;
                existingProduct.Discontinued = product.Discontinued;

                dtx.Entry(existingProduct).State = EntityState.Modified;
                await dtx.SaveChangesAsync();

                return Ok(existingProduct);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: Delete product
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                ProductDBContext dtx = new ProductDBContext();
                var product = await dtx.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }

                dtx.Products.Remove(product);
                await dtx.SaveChangesAsync();

                return Ok($"Product with ID {id} was deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Helper method to check if product exists
        private bool ProductExists(int id)
        {
            ProductDBContext dtx = new ProductDBContext();
            return dtx.Products.Any(e => e.ProductID == id);
        }

    }
}

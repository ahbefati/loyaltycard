using loyaltycard.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;


namespace loyaltycard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public ProductController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var Products = await appDbContext.Products.ToListAsync();
            var activeProducts = Products.FindAll(e => e.Status == "Active");
            return Ok(activeProducts);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var Product = await appDbContext.Products.FirstOrDefaultAsync(e => e.Id == id);
            if (Product != null)
            {
                return Ok(Product);
            }
            return NotFound();
        }
        [HttpGet("inactive")]
        public async Task<ActionResult<List<Product>>> GetInactiveProducts()
        {
            var Products = await appDbContext.Products.ToListAsync();
            var inactiveProducts = Products.FindAll(e => e.Status == "Inactive");
            if (Products != null)
            {
                return Ok(inactiveProducts);
            }
            return NotFound();
        }
        //Add
        [HttpPost]
        public async Task<ActionResult<int>> AddProduct(Product newProduct)
        {
            if (newProduct != null)
            {
                try
                {
                    newProduct.Status = "Active";
                    appDbContext.Products.Add(newProduct);
                    await appDbContext.SaveChangesAsync();


                   
                    return Ok(newProduct.Id);
                }
                catch (Exception ex) { NotFound(ex); }
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteProduct(int id)
        {
            var Product = await appDbContext.Products.FirstOrDefaultAsync(e => e.Id == id);
            if (Product != null)
            {
                try
                {
                    Product.Status="Inactive";
                    await appDbContext.SaveChangesAsync();


                    return Ok(true);
                }
                catch (Exception ex) { return NotFound(ex); }
            }
            else { return BadRequest(false); }
        }


        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct(Product updatedProduct)
        {
            if (updatedProduct != null)
            {
                var Product = await appDbContext.Products.FirstOrDefaultAsync(e => e.Id == updatedProduct.Id);
                if (Product != null)
                {
                    try
                    {
                        Product.Id = updatedProduct.Id;
                        Product.Name = updatedProduct.Name;
                        Product.Category = updatedProduct.Category;
                        Product.Brand = updatedProduct.Brand;
                        Product.Price = updatedProduct.Price;
                        Product.Type = updatedProduct.Type;
                        Product.Description = updatedProduct.Description;
                        if (updatedProduct.Status == "Active" || updatedProduct.Status == "Inactive") { Product.Status = updatedProduct.Status; };



                        await appDbContext.SaveChangesAsync();


                        return Ok(Product);
                    }
                    catch (Exception ex) { BadRequest(ex); }

                }
                else { return NotFound(); }
            }
            return BadRequest();
        }

    }
}
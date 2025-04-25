using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;

        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }

        // GET: api/Stock
        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockService.GetAllStocksAsync();
            return Ok(stocks);
        }

        // GET: api/Stock/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        // POST: api/Stock
        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdStock = await _stockService.AddStockAsync(stock);
            return CreatedAtAction(nameof(GetStockById), new { id = createdStock.IdStock }, createdStock);
        }

        // PUT: api/Stock/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] Stock stock)
        {
            if (id != stock.IdStock)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _stockService.UpdateStockAsync(stock);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Stock/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var result = await _stockService.DeleteStockAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Stock/ByLot/{lotId}
        [HttpGet("ByLot/{lotId}")]
        public async Task<IActionResult> GetStocksByLotId(int lotId)
        {
            var stocks = await _stockService.GetStocksByLotIdAsync(lotId);
            return Ok(stocks);
        }

        // GET: api/Stock/ByEntrepot/{entrepotId}
        [HttpGet("ByEntrepot/{entrepotId}")]
        public async Task<IActionResult> GetStocksByEntrepotId(int entrepotId)
        {
            var stocks = await _stockService.GetStocksByEntrepotIdAsync(entrepotId);
            return Ok(stocks);
        }
    }
}
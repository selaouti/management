using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Services;
using StockManagement.Domain.Entities;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MouvementStockController : ControllerBase
    {
        private readonly MouvementStockService _mouvementStockService;

        public MouvementStockController(MouvementStockService mouvementStockService)
        {
            _mouvementStockService = mouvementStockService;
        }

        // GET: api/MouvementStock/ByLot/{lotId}
        [HttpGet("ByLot/{lotId}")]
        public async Task<IActionResult> GetMouvementsByLot(int lotId)
        {
            var mouvements = await _mouvementStockService.GetMouvementsByLotAsync(lotId);
            return Ok(mouvements);
        }

        // GET: api/MouvementStock/ByEntrepot/{entrepotId}
        [HttpGet("ByEntrepot/{entrepotId}")]
        public async Task<IActionResult> GetMouvementsByEntrepot(int entrepotId)
        {
            var mouvements = await _mouvementStockService.GetMouvementsByEntrepotAsync(entrepotId);
            return Ok(mouvements);
        }
    }
}
// Controllers/TableController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Wedding.Web.Entities;
using Wedding.Web.Services;

namespace Wedding.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private readonly TableService _tableService;

        public TableController(TableService tableService)
        {
            _tableService = tableService;
        }

        [HttpPost]
        public IActionResult AddEntity(CustomEntity entity)
        {
            _tableService.AddEntity("OSA", entity);
            return Ok();
        }

        [HttpGet("{partitionKey}")]
        public ActionResult<List<CustomEntity>> GetEntities(string partitionKey)
        {
            var entities = _tableService.QueryEntities<CustomEntity>("OSA", partitionKey);
            return Ok(entities);
        }

        [HttpGet("all")]
        public ActionResult<List<CustomEntity>> GetEntities()
        {
            var entities = _tableService.GetAllEntities<CustomEntity>("OSA");
            return Ok(entities);
        }


        [HttpDelete("{partitionKey}/{rowKey}")]
        public IActionResult DeleteEntity(string partitionKey, string rowKey)
        {
            _tableService.DeleteEntity("OSA", partitionKey, rowKey);
            return NoContent();
        }
    }

}
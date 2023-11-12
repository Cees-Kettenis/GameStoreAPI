using GameStoreAPi.Data;
using GameStoreAPi.Modals.SKU;
using GameStoreAPi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameStoreAPi.Controllers
{
    [Route("api/[controller]")]
    public class SKUController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            AppDBContext dbService = (AppDBContext)HttpContext.RequestServices.GetService(typeof(AppDBContext));
            var repository = new RepositoryHelper<SKU>(dbService);
            var users = repository.Query().ToList();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(Guid Id)
        {
            AppDBContext dbService = (AppDBContext)HttpContext.RequestServices.GetService(typeof(AppDBContext));
            var user = dbService.SKUs.Where(x => x.id == Id).FirstOrDefault();
            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateSKU([FromBody]SKU sku)
        {
            AppDBContext dbService = (AppDBContext)HttpContext.RequestServices.GetService(typeof(AppDBContext));
            var repository = new RepositoryHelper<SKU>(dbService);

            var ExistingEntry = repository.Query().Where(x => x.number == sku.number).FirstOrDefault();
            if(ExistingEntry != null)
            {
                return BadRequest(new ReturnObject() { 
                    ErrorCode = (Int32)HttpStatusCode.BadRequest,
                    Message = "Number is already in use.",
                    Result = ExistingEntry
                });
            }
            var CreatedEntity = repository.Create(sku);
            return Ok(new ReturnObject() { 
                ErrorCode = (Int32)HttpStatusCode.Created,
                Message = "Entity Succesfully Created",
                Result = CreatedEntity
            });
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateSKU(Guid Id, [FromBody] SKU sku)
        {
            AppDBContext dbService = (AppDBContext)HttpContext.RequestServices.GetService(typeof(AppDBContext));
            var repository = new RepositoryHelper<SKU>(dbService);
            var ExistingEntry = repository.Query().Where(x => x.id == Id).FirstOrDefault();
            if (ExistingEntry == null)
            {
                return BadRequest(new ReturnObject()
                {
                    ErrorCode = (Int32)HttpStatusCode.BadRequest,
                    Message = "Unable to find SKU.",
                });
            }
            var UpdatedEntity = repository.Update(sku);
            return Ok(new ReturnObject()
            {
                ErrorCode = (Int32)HttpStatusCode.OK,
                Message = "Entity Succesfully updated",
                Result = UpdatedEntity
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteSKU(Guid Id)
        {
            AppDBContext dbService = (AppDBContext)HttpContext.RequestServices.GetService(typeof(AppDBContext));
            var repository = new RepositoryHelper<SKU>(dbService);
            var ExistingEntry = repository.Query().Where(x => x.id == Id).FirstOrDefault();
            if (ExistingEntry == null)
            {
                return BadRequest(new ReturnObject()
                {
                    ErrorCode = (Int32)HttpStatusCode.BadRequest,
                    Message = "Unable to find SKU.",
                });
            }
            repository.Delete(ExistingEntry);
            return Ok(new ReturnObject()
            {
                ErrorCode = (Int32)HttpStatusCode.OK,
                Message = "Entity Succesfully Deleted",
                
            });
        }

    }
}

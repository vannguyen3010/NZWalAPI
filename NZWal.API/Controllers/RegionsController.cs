using Microsoft.AspNetCore.Mvc;
using NZWal.API.Data;
using NZWal.API.Models.DTO;

namespace NZWal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalDbContext dbContext;

        public RegionsController(NZWalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET ALL REGIONS
        //GET : https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAllRegion()
        {
            //Get Data From Database - Domain models
            var regionsDomain = dbContext.Regions.ToList();

            //Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var item in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    RegionImageUrl = item.RegionImageUrl,
                });
            }

            return Ok(regionsDto);
        }

        //GET SINGLE REGION (Get Resion By ID)
        //GET : https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            //var region = dbContext.Regions.Find(id);
            //Get Region Domain Model From Database
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
            {
                return BadRequest("Không tìm thấy người dùng");
            }

            // Map/Convert Region Domain Model to Region Dto

            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            //Return DTO back to client
            return Ok(regionDto);
        }
    }
}

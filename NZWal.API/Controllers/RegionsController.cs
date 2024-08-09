using Microsoft.AspNetCore.Mvc;
using NZWal.API.Data;
using NZWal.API.Models.Domain;
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

        //POST To Create New Region
        //POST: https://localhost:portnumber/api/region
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Covert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };
            //Use Domain Model to create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map Domain Model back to Dto
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            //CreatedAtAction một phương thức trả về HTTP status code 201 (Created)
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            //Phương thức này sử dụng nameof(GetById), để chỉ định rằng URL trả về sẽ trỏ đến hành động GetById (thường dùng để lấy chi tiết của Region theo ID).
        }
    }
}

﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWal.API.CustomActionFilter;
using NZWal.API.Data;
using NZWal.API.Models.Domain;
using NZWal.API.Models.DTO;
using NZWal.API.Repositories;
using System.Text.Json;

namespace NZWal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalDbContext dbContext, IRegionRepository regionRepository,IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        #region GetAllRegions
        //GET ALL REGIONS
        //GET : https://localhost:portnumber/api/regions
        [HttpGet("GetAllRegions")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllRegion()
        {
            //Get Data From Database - Domain models
            var regionsDomain = await regionRepository.GetAllRegionsAsync();

            //return DTOs

            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));

        }
        #endregion

        #region GetByid
        //GET SINGLE REGION (Get Resion By ID)
        //GET : https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("GetById/{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            //Get Region Domain Model From Database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return BadRequest("Không tìm thấy người dùng");
            }

            //Return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
        #endregion

        #region CreateRegion

        //POST To Create New Region
        //POST: https://localhost:portnumber/api/region
        [HttpPost("CreateRegion")]
        [ValidateModel] //Kiểm tra validate model (ValidateModelAttribute)
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Covert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            //Use Domain Model to create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map Domain Model back to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            //CreatedAtAction một phương thức trả về HTTP status code 201 (Created)
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            //Phương thức này sử dụng nameof(GetById), để chỉ định rằng URL trả về sẽ trỏ đến hành động GetById (thường dùng để lấy chi tiết của Region theo ID).
        }

        #endregion

        #region UpdateRegion
        //Update Region
        //Put: https://localhost:portnumber/api/region/{id}
        [HttpPut]
        [Route("UpdateRegion/{id:Guid}")]
        [ValidateModel] //Kiểm tra validate model (ValidateModelAttribute)
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            //Check if region exits
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return BadRequest("Không tìm thấy id");
            }

            //Convert Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
        #endregion

        #region DeleteRegion
        //Delete Region
        //Delete: https://localhost:portnumber/api/region/{id}
        [HttpDelete]
        [Route("DeleteRegion/{id:Guid}")]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return BadRequest("Không tìm thấy id");
            }

            //map Domain Model to DTO
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
        #endregion

    }
}

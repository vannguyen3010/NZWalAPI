using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWal.API.CustomActionFilter;
using NZWal.API.Models.Domain;
using NZWal.API.Models.DTO;
using NZWal.API.Repositories;

namespace NZWal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //CREATE Walk
        //POST: api/Walks/CreateWalks
        [HttpPost("CreateWalk")]
        [ValidateModel] //Kiểm tra validate model (ValidateModelAttribute)
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            //Lưu trữ 
            await walkRepository.CreateWalkAsync(walkDomainModel);

            //Map Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        //GET Walk
        //GET: api/Walks/GetAllWalks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet("GetAllWalks")]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            //Get Data From Database - Domain models
            //sortBy: Sắp xếp, isAscending : Giá trị tăng dần 
            var walksDomainModel = await walkRepository.GetAllWalksAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //Map Domain Model to Dto
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        //GET Single
        //GET: api/Walks/GetWalkById/{id}
        [HttpGet]
        [Route("GetById/{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            //Map Dto to Domain model
            var walksDomainModel = await walkRepository.GetWalkByIdAsync(id);
            if (walksDomainModel == null)
            {
                return BadRequest("Không tìm thấy id người dùng");
            }

            //Map Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }

        //Update Walk id
        //PUT: api/Walks/UpdateWalk/{id}
        [HttpPut]
        [Route("UpdateWalk/{id:Guid}")]
        [ValidateModel] //Kiểm tra validate model (ValidateModelAttribute)
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //Map DTO to Domain model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateWalkAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return BadRequest("Không tìm thấy id người dùng");
            }

            //Map Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Delete Walk id
        //DELETE: api/Walks/DeleteWalk/{id}
        [HttpDelete]
        [Route("DeleteWalk/{id:Guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteWalkAsync(id);
            if(walkDomainModel == null)
            {
                return BadRequest("Không tìm thấy id người dùng!!");
            }

            //Map Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}

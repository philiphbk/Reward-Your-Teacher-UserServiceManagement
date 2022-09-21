using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RYTUserManagementService.Domain.RepoInterfaces;
using RYTUserManagementService.Dto;
using RYTUserManagementService.Models;



namespace RYTUserManagementService.API.Controllers
{
    [ApiController]
    [Route("[controller]/api/v1")]
    public class SchoolController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SchoolController> _logger;
        private readonly IMapper _mapper;

        public SchoolController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SchoolController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }



        // <summary>
        // 
        // </summary>
        // <param name = "id" ></ param >
        // < returns ></ returns >

        //GET: School
     
       [HttpGet("{id}", Name="GetSchoolById")]
       [ProducesResponseType(200)]
       [ProducesResponseType(400)]
       [ProducesResponseType(404)]
        public async Task<IActionResult> GetSchoolById(string id)
        {
            try
            {
                var school = await _unitOfWork.Schools.Get(q => q.Id == id);
                var result = _mapper.Map<SchoolViewDto>(school);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetSchoolById)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }

        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: AllSchools
        [HttpGet("GetAllSchools")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllSchools()
        {
            try
            {
                var schools = await _unitOfWork.Schools.GetAll();
                
                var results = _mapper.Map<IList<SchoolViewDto>>(schools);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetAllSchools)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Post: CreateSchool
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("CreateSchool")]
        public async Task<IActionResult> CreateSchool([FromBody] SchoolCreateDto schoolcreate)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateSchool)}");
                return BadRequest(ModelState);
            }

            try
            {
               
                var school = _mapper.Map<School>(schoolcreate);
                school.Id = Guid.NewGuid().ToString();
                await _unitOfWork.Schools.Insert(school);
                await _unitOfWork.Save();
                  
                return CreatedAtRoute(nameof(GetSchoolById), new { id = school.Id }, school);

            }  
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(CreateSchool)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Put: UpdateSchool
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("UpdateSchool")]
        public async Task<IActionResult> UpdateSchool(string id, [FromBody] SchoolCreateDto school)
        {
            if (!ModelState.IsValid || id == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateSchool)}");
                return BadRequest(ModelState);
            }

            try
            {
                var getSchool = await _unitOfWork.Schools.Get(q => q.Id == id);
                if (getSchool == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateSchool)}");
                    return BadRequest("Submitted Data is Invalid");
                }

                _mapper.Map(school, getSchool);
                _unitOfWork.Schools.Update(getSchool);
                await _unitOfWork.Save();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(UpdateSchool)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Delete: DeleteSchool
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("DeleteSchool")]
        public async Task<IActionResult> DeleteSchool(string id)
        {
            if (id == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteSchool)}");
                return BadRequest(ModelState);
            }

            try
            {
                var school = await _unitOfWork.Schools.Get(q => q.Id == id);
                if (school == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteSchool)}");
                    return BadRequest("Submitted Data is Invalid");
                }


                await _unitOfWork.Schools.Delete(school.Id);
                await _unitOfWork.Save();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(DeleteSchool)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }

    }
}


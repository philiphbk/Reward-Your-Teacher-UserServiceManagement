using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using RYTUserManagementService.Domain.RepoInterfaces;
using RYTUserManagementService.Dto;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.API.Controllers
{
    [ApiController]
    [Route("[controller]/api/v1")]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IEmailSender _emailSender;

        public TeacherController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TeacherController> logger, UserManager<ApiUser> userManager, IEmailSender emailSender)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: Teacher
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetTeacherById(string id)
        {
            try
            {
                var teacher = await _unitOfWork.Teachers.Get(q => q.Id == id, new List<string> { "Teachers" });
                var result = _mapper.Map<TeacherDto>(teacher);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetTeacherById)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: AllTeachers
        [HttpGet("GetAllTeachers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teachers = await _unitOfWork.Teachers.GetAll();
                var results = _mapper.Map<IList<TeacherDto>>(teachers);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetAllTeachers)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Post: CreateTeacher
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("CreateTeacher")]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherDto teacherDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateTeacher)}");
                return BadRequest(ModelState);
            }

            try
            {
                var teacher = _mapper.Map<Teacher>(teacherDto);
                await _unitOfWork.Teachers.Insert(teacher);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetTeacher", new { id = teacher.Id }, teacher);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(CreateTeacher)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Put: UpdateTeacher
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("UpdateTeacher")]
        public async Task<IActionResult> UpdateTeacher(string id, [FromBody] UpdateTeacherDto teacherDto)
        {
            if (!ModelState.IsValid || id == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateTeacher)}");
                return BadRequest(ModelState);
            }

            try
            {
                var updateteach = await _unitOfWork.Teachers.Get(q => q.Id == id);
                if (updateteach == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateTeacher)}");
                    return BadRequest("Submitted Data is Invalid");
                }

                _mapper.Map(teacherDto, updateteach);
                _unitOfWork.Teachers.Update(updateteach);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(UpdateTeacher)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Delete: DeleteTeacher
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("DeleteTeacher")]
        public async Task<IActionResult> DeleteTeacher(string id)
        {
            if (id == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteTeacher)}");
                return BadRequest(ModelState);
            }

            try
            {
                var teacher = await _unitOfWork.Teachers.Get(q => q.Id == id);
                if (teacher == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteTeacher)}");
                    return BadRequest("Submitted Data is Invalid");
                }


                await _unitOfWork.Teachers.Delete(teacher.Id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(DeleteTeacher)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }

            
        }

    //    //[Authorize]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    [HttpPost("ForgotPassword")]
    //    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var user = await _userManager.FindByEmailAsync(model.Email);
    //            if (user == null)
    //            {
    //                return NotFound();
    //            }

    //            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
    //            var callbackurl = Url.Action("ResetPassword", "Student", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

    //            await _emailSender.SendEmailAsync(model.Email, "Reset Password - Identity Manager", "Please reset your password" + callbackurl);
    //        }

    //        return Ok();
    //    }

    //    //[Authorize]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    [HttpPost("ResetPassword")]

    //    public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var user = await _userManager.FindByEmailAsync(model.Email);
    //            if (user == null)
    //            {
    //                return NotFound();
    //            }

    //            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
    //            if (result.Succeeded)
    //            {
    //                return Ok("Password Reset Successfully");
    //            }
    //        }

    //        return Ok();
    //    }

    }
}


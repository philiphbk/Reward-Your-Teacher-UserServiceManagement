using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using RYTUserManagementService.Domain.RepoInterfaces;
using RYTUserManagementService.Models;
using RYTUserManagementService.Dto.TeacherDto;
using RYTUserMangementService.Services.Interfaces;
using RYTUserManagementService.Common.Utilities;
using RYTUserManagementService.Dto;

namespace RYTUserManagementService.API.Controllers
{
    [ApiController]
    [Route("[controller]/api/v1")]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherController> _logger;
        private readonly UserManager<Teacher> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IHttpService _service;
        private readonly IConfiguration _config;

        public TeacherController(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<TeacherController> logger, UserManager<Teacher> userManager,
            IEmailSender emailSender, IHttpService service, IConfiguration config)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
            _service = service;
            _config = config;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: Teacher
        [HttpGet("{id}", Name = "GetTeacherById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetTeacherById(string id)
        {
            try
            {
                var teacher = await _unitOfWork.Teachers.Get(q => q.Id == id);
                var result = _mapper.Map<CreateTeacherDto>(teacher);
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
                var results = _mapper.Map<IList<TeachersDto>>(teachers);
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
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("CreateTeacher")]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherDto teacherDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateTeacher)}");
                return BadRequest(ModelState);
            }

            try
            {
                var teacher = _mapper.Map<Teacher>(teacherDto);
                teacher.UserName = teacher.Email;
                var res = await _userManager.CreateAsync(teacher, teacher.PasswordHash);
                await _unitOfWork.Save();

                var walletDto = new UserWalletDto()
                {
                    UserId = teacher.Id,
                    Currency = "NGN",
                    Status = true,
                    UserFullName = $"{teacher.LastName} {teacher.FirstName}",
                };
                var jsonRequest = new JsonContentPostRequest<UserWalletDto>()
                {
                    Url = _config["walletCreateUrl"],
                    Data = walletDto
                };
                var response = await _service.SendPostRequest<WalletResponse, UserWalletDto>(jsonRequest);
                if (response.Status == true)
                {
                    return CreatedAtRoute(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
                }
                return BadRequest(new { message = "something went wrong with creating wallet" });
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
        //[Authorize]
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

                return Ok();
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
        //[Authorize]
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

                return Ok();
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


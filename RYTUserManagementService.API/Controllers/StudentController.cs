using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using RYTUserManagementService.Common.Utilities;
using RYTUserManagementService.Domain.RepoInterfaces;
using RYTUserManagementService.Dto;
using RYTUserManagementService.Dto.StudentDto;
using RYTUserManagementService.Models;
using RYTUserMangementService.Services.Interfaces;
using System.Security.Cryptography;

namespace RYTUserManagementService.API.Controllers
{
    [ApiController]
    [Route("[controller]/api/v1")]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentController> _logger;
        private readonly UserManager<Student> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;
        private readonly IHttpService _service;

        public StudentController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StudentController> logger, 
            UserManager<Student> userManager, IEmailSender emailSender, IConfiguration config,
            IHttpService service)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
            _config = config;
            _service = service;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}", Name="GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetStudentById(string id)
        {
            try
            {
                var student = await _unitOfWork.Students.Get(q => q.Id == id);
                var result = _mapper.Map<StudentsDto>(student);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetStudentById)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: AllStudents
        [HttpGet("GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _unitOfWork.Students.GetAll();
                var results = _mapper.Map<IList<StudentsDto>>(students);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetAllStudents)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
            
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Post: CreateStudent
        [AllowAnonymous]
        [HttpPost("CreateStudent")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStudent([FromBody] StudentsDto studentDto)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateStudent)}");
                return BadRequest(ModelState);
            }

            try
            {
                var student = _mapper.Map<Student>(studentDto);
                student.UserName = studentDto.Email; 
                var res = await _userManager.CreateAsync(student, student.PasswordHash);
                await _unitOfWork.Save();

                var walletDto = new UserWalletDto()
                {
                    UserId = student.Id,
                    Currency = "NGN",
                    Status = true,
                    UserFullName = $"{student.LastName} {student.FirstName}",
                };
                var jsonRequest = new JsonContentPostRequest<UserWalletDto>()
                {
                    Url = _config["walletCreateUrl"],
                    Data = walletDto
                };
                var response = await _service.SendPostRequest<WalletResponse, UserWalletDto>(jsonRequest);
                if (response.Status == true)
                {
                    return CreatedAtRoute(nameof(GetStudentById), new { id = student.Id }, student);
                }
                return BadRequest(new { message = "something went wrong with creating wallet" });

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(CreateStudent)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Put: UpdateStudent
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(string id, [FromBody] UpdateStudentDto student)
        {

            if (!ModelState.IsValid || id == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateStudent)}");
                return BadRequest(ModelState);
            }

            try
            {
                var updatestud = await _unitOfWork.Students.Get(q => q.Id == id);
                if (updatestud == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateStudent)}");
                    return BadRequest("Submitted Data is Invalid");
                }

                _mapper.Map(student, updatestud);
                _unitOfWork.Students.Update(updatestud);
                await _unitOfWork.Save();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(UpdateStudent)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Delete: DeleteStudent
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            if (id == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteStudent)}");
                return BadRequest(ModelState);
            }

            try
            {
                var student = await _unitOfWork.Students.Get(q => q.Id == id);
                if (student == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteStudent)}");
                    return BadRequest("Submitted Data is Invalid");
                }

                await _unitOfWork.Students.Delete(student.Id);
                await _unitOfWork.Save();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(DeleteStudent)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }

                       
        }

        



        //[Authorize]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[HttpPost("ResetPassword")]
        //public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        //        if (result.Succeeded)
        //        {
        //            return Ok("Password Reset Successfully");
        //        }
        //    }

        //    return Ok();
        //}

        /*[ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPost("[controller]/ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
           if(ModelState.IsValid)
           {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    return NotFound();
                }
            
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Student", new { userId=user.Id, code=code }, protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(model.Email, "Reset Password - Identity Manager", "Please reset your password" + callbackurl);
           }

            return Ok();
        }
*/

       /* [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPost("[controller]/ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound();
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if(result.Succeeded)
                {
                    return Ok("Password Reset Successfully");
                }
            }

            return Ok();
        }*/

    }
    
}

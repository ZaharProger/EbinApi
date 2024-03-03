using EbinApi.Models.Db;
using EbinApi.Models.Http;
using EbinApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbinApi.Controllers
{
    [ApiController]
    [Route("api/companies")]
    [Authorize]
    public class CompanyController(CompanyService companyService, UserService userService)
    : EbinController(userService)
    {
        private readonly CompanyService _companyService = companyService;

        [HttpGet]
        public async Task<IActionResult> GetCompaniesShortInfo()
        {
            var authorizedUser = await CheckSession();
            IActionResult response = Unauthorized(new BaseResponse()
            {
                Message = ""
            });

            if (authorizedUser != null)
            {
                var companies = await _companyService.GetCompaniesShortInfo();
                response = new JsonResult(new CollectionResponse<Company>()
                {
                    Message = "",
                    Objects = companies
                });
            }

            return response;
        }
    }

}
using DataExporter.Dtos;
using DataExporter.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataExporter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PoliciesController : ControllerBase
    {
        private PolicyService _policyService;

        public PoliciesController(PolicyService policyService) 
        { 
            _policyService = policyService;
        }

        [HttpPost]
        public async Task<IActionResult> PostPolicies([FromBody]CreatePolicyDto createPolicyDto)
        {         
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetPolicies()
        {
            return Ok();
        }

        [HttpGet("GetPolicy/{policyId}")]
        public async Task<IActionResult> GetPolicy(int policyId)
        {
            //added a missed await keyword and made a variable for result just for more convenience
            var policy = await _policyService.ReadPolicyAsync(policyId);

            if (policy is null)
            {
                return NotFound();
            }

            return Ok(policy);
        }


        [HttpPost("export")]
        public async Task<IActionResult> ExportData([FromQuery]DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok();
        }
    }
}

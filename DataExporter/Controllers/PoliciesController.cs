using DataExporter.Abstractions;
using DataExporter.Dtos;
using Microsoft.AspNetCore.Mvc;
using static DataExporter.Constants.Endpoints;

namespace DataExporter.Controllers
{
    [ApiController]
    [Route(Policy.PolicyRoute)]
    public class PoliciesController : ControllerBase
    {
        private IPolicyService _policyService;

        public PoliciesController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpPost(Policy.PostPolicies)]
        public async Task<IActionResult> PostPolicies([FromBody] CreatePolicyDto createPolicyDto)
        {
            var cratedPolicy = await _policyService.CreatePolicyAsync(createPolicyDto);

            if (createPolicyDto is null)
            {
                //TODO
                //mb should be changed to another request result
                return BadRequest();
            }

            return Ok(cratedPolicy);
        }


        [HttpGet(Policy.GetPolicies)]
        public async Task<IActionResult> GetPolicies()
        {
            var policies = await _policyService.ReadPoliciesAsync();

            if (!policies.Any())
            {
                return NoContent();
            }

            return Ok(policies);
        }

        [HttpGet(Policy.GetPolicy)]
        public async Task<IActionResult> GetPolicy([FromQuery]int policyId)
        {
            //added a missed await keyword and made a variable for result just for more convenience
            var policy = await _policyService.ReadPolicyAsync(policyId);

            if (policy is null)
            {
                return NotFound();
            }

            return Ok(policy);
        }


        [HttpPost(Policy.ExportData)]
        public async Task<IActionResult> ExportData([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var exportedPolicies = await _policyService.ExportDataAsync(startDate, endDate);

            return Ok(exportedPolicies);
        }
    }
}

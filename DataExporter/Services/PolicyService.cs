using DataExporter.Dtos;
using Microsoft.EntityFrameworkCore;


namespace DataExporter.Services
{
    public class PolicyService
    {
        private ExporterDbContext _dbContext;

        public PolicyService(ExporterDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Creates a new policy from the DTO.
        /// </summary>
        /// <param name="policy"></param>
        /// <returns>Returns a ReadPolicyDto representing the new policy, if succeded. Returns null, otherwise.</returns>
        public async Task<ReadPolicyDto?> CreatePolicyAsync(CreatePolicyDto createPolicyDto)
        {
            return await Task.FromResult(new ReadPolicyDto());
        }

        /// <summary>
        /// Retrives all policies.
        /// </summary>
        /// <returns>Returns a list of ReadPoliciesDto.</returns>
        public async Task<IList<ReadPolicyDto>> ReadPoliciesAsync()
        {
            var policies = await _dbContext.Policies
                .Select(x => new ReadPolicyDto()
                {
                    Id = x.Id,
                    PolicyNumber = x.PolicyNumber,
                    Premium = x.Premium,
                    StartDate = x.StartDate
                })
                .ToListAsync();
            return policies;
        }

        /// <summary>
        /// Retrieves a policy by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a ReadPolicyDto.</returns>
        public async Task<ReadPolicyDto?> ReadPolicyAsync(int id)
        {
            //Source code
            //var policy = await _dbContext.Policies.SingleAsync(x => x.Id == id);
            //if (policy == null)
            //{
            //    return null;
            //}

            //var policyDto = new ReadPolicyDto()
            //{
            //    Id = policy.Id,
            //    PolicyNumber = policy.PolicyNumber,
            //    Premium = policy.Premium,
            //    StartDate = policy.StartDate
            //};

            //return policyDto;

            //Updated code
            var policyDto = await _dbContext.Policies
                .Where(x => x.Id == id)
                //added select method to avoid creating unnecessary objects
                //and immediately convert the entity to a dto
                .Select(x => new ReadPolicyDto()
                {
                    Id = x.Id,
                    PolicyNumber = x.PolicyNumber,
                    Premium = x.Premium,
                    StartDate = x.StartDate
                })
                //changed SingleAsync to FirstOrDefaultAsync bc SingleAsync causes an exception if table contains not only 1 record
                .FirstOrDefaultAsync();

            return policyDto;
        }
    }
}

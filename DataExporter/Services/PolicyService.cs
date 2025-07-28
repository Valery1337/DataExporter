using DataExporter.Dtos;
using DataExporter.Model;
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
        /// <param name="createPolicyDto"></param>
        /// <returns>Returns a ReadPolicyDto representing the new policy, if succeded. Returns null, otherwise.</returns>
        public async Task<ReadPolicyDto?> CreatePolicyAsync(CreatePolicyDto createPolicyDto)
        {
            if (createPolicyDto is null)
            {
                return null;
            }

            var policy = new Policy()
            {
                PolicyNumber = createPolicyDto.PolicyNumber,
                Premium = createPolicyDto.Premium,
                StartDate = createPolicyDto.StartDate
            };

            //TODO
            //I'm not sure about this structure bc for my opinion it will be better just to handle ex here og globally,
            //but as I understood from "Returns a ReadPolicyDto representing the new policy, if succeded. Returns null, otherwise"
            //if we have problems with saving or adding we should just return null
            try
            {
                await _dbContext.Policies.AddAsync(policy);
                await _dbContext.SaveChangesAsync();

                return new ReadPolicyDto()
                {
                    Id = policy.Id,
                    PolicyNumber = policy.PolicyNumber,
                    Premium = policy.Premium,
                    StartDate = policy.StartDate
                };
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves all policies.
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

        /// <summary>
        /// Retrieves all policies that have a start date between specified dates
        /// </summary>
        /// <returns>Retures a list of ExportDto</returns>
        public async Task<IList<ExportDto>> ExportDataAsync(DateTime startDate, DateTime endDate)
        {
            var exportedPolicies = await _dbContext.Policies
                .Include(x => x.Notes)
                .Where(x => x.StartDate >= startDate && x.StartDate <= endDate)
                .Select(x => new ExportDto()
                {
                    PolicyNumber = x.PolicyNumber,
                    Premium = x.Premium,
                    StartDate = x.StartDate,
                    Notes = x.Notes.Select(x => x.Text).ToList()
                })
                .ToListAsync();

            return exportedPolicies;
        }
    }
}

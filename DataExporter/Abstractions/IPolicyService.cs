using DataExporter.Dtos;

namespace DataExporter.Abstractions;
public interface IPolicyService
{
    /// <summary>
    /// Creates a new policy from the DTO.
    /// </summary>
    /// <param name="createPolicyDto"></param>
    /// <returns>Returns a ReadPolicyDto representing the new policy, if succeded. Returns null, otherwise.</returns>
    Task<ReadPolicyDto?> CreatePolicyAsync(CreatePolicyDto createPolicyDto);

    /// <summary>
    /// Retrieves all policies.
    /// </summary>
    /// <returns>Returns a list of ReadPoliciesDto.</returns>
    Task<IList<ReadPolicyDto>> ReadPoliciesAsync();

    /// <summary>
    /// Retrieves a policy by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns a ReadPolicyDto.</returns>
    Task<ReadPolicyDto?> ReadPolicyAsync(int id);

    /// <summary>
    /// Retrieves all policies that have a start date between specified dates
    /// </summary>
    /// <returns>Retures a list of ExportDto</returns>
    Task<IList<ExportDto>> ExportDataAsync(DateTime startDate, DateTime endDate);
}
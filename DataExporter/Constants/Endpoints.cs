namespace DataExporter.Constants;
public readonly struct Endpoints
{
    public readonly struct Policy
    {
        public const string PolicyRoute = "policies";

        public const string PostPolicies = nameof(PostPolicies);
        public const string GetPolicies = nameof(GetPolicies);
        public const string GetPolicy = nameof(GetPolicy);
        public const string ExportData = nameof(ExportData);
    }
}
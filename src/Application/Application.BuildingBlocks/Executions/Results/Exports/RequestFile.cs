namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results
{
    public record RequestFile(byte[] Bytes, RequestFileTypes Type, string FileName, bool IncludeTime = true) 
    {
       
    }
}

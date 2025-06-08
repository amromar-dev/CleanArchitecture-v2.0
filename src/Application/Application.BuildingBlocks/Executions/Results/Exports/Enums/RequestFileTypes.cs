namespace CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results
{
    public enum RequestFileTypes
    {
        Csv = 1,
        Excel,
        Pdf
    }

    public static class RequestFileTypesExtensions
    {
        public static string GetMimeType(this RequestFileTypes type)
        {
            return type switch
            {
                RequestFileTypes.Csv => "text/csv",
                RequestFileTypes.Excel => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                RequestFileTypes.Pdf => "application/pdf",
                _ => throw new NotImplementedException(),
            };
        }

        public static string GetExtension(this RequestFileTypes type)
        {
            return type switch
            {
                RequestFileTypes.Csv => "csv",
                RequestFileTypes.Excel => "xlsx",
                RequestFileTypes.Pdf => "pdf",
                _ => throw new NotImplementedException(),
            };
        }
    }
}

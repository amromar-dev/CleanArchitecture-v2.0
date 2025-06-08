using CleanArchitectureTemplate.SharedKernels.Exceptions;
using CleanArchitectureTemplate.SharedKernels.Localizations;
using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSorting"></typeparam>
    public record PageOption<TSorting> where TSorting : Enum
    {
        [JsonConstructor]
        public PageOption(int PageNumber, int PageSize, TSorting SortingBy, PageSortingType SortingType)
        {
            if (PageNumber <= 0)
                throw new FieldValidationException(nameof(PageNumber), Localization.ValueShouldBeGreaterThanZero);

            if (PageSize <= 0)
                throw new FieldValidationException(nameof(PageSize), Localization.ValueShouldBeGreaterThanZero);

            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.SortingBy = SortingBy;
            this.SortingType = SortingType == 0 ? PageSortingType.Ascending : SortingType;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public PageSortingType SortingType { get; set; } = PageSortingType.Ascending;

        public TSorting SortingBy { get; set; }
    }
}

using Application.Common.Mappings;
using Domain.Common;

namespace Application.Common.ViewModels
{
    public class CreatedObject : IMapFrom<BaseEntity>
    {
        public long Id { get; set; }
    }
}

namespace GradeApi.Infrastructure.Mappers
{
    using AutoMapper;

    public class DomainDtoProfile : Profile
    {
        public DomainDtoProfile()
            : base(nameof(DomainDtoProfile))
        {
            CreateMap<Domain.Entities.Student, Presentation.Dtos.StudentDto>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => $"S{src.Id}"));

            CreateMap<Presentation.Dtos.StudentDto, Domain.Entities.Student>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Code.Substring(1))));

            CreateMap<Domain.Entities.StudentAddress, Presentation.Dtos.StudentAddressDto>();

            CreateMap<Presentation.Dtos.StudentAddressDto, Domain.Entities.StudentAddress>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}

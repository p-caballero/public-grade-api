namespace GradeApi.Infrastructure.Mappers
{
    using AutoMapper;

    public class DomainPersistenceProfile : Profile
    {
        public DomainPersistenceProfile()
            : base(nameof(DomainPersistenceProfile))
        {
            CreateMap<Domain.Entities.Student, Persistence.Entities.Student>()
                .ForMember(dest => dest.Weight, opt => opt.Ignore())
                .ForMember(dest => dest.Height, opt => opt.Ignore())
                .ForMember(dest => dest.StudentCourses, opt => opt.Ignore());

            CreateMap<Persistence.Entities.Student, Domain.Entities.Student>();

            CreateMap<Domain.Entities.StudentAddress, Persistence.Entities.StudentAddress>()
                .ForMember(dest => dest.Student, opt => opt.Ignore())
                .ForMember(dest => dest.StudentId, opt => opt.Ignore());

            CreateMap<Persistence.Entities.StudentAddress, Domain.Entities.StudentAddress>();
        }
    }
}

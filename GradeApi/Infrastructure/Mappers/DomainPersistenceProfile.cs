namespace GradeApi.Infrastructure.Mappers
{
    using AutoMapper;

    public class DomainPersistenceProfile : Profile
    {
        public DomainPersistenceProfile()
            : base(nameof(DomainPersistenceProfile))
        {
            CreateMap<Domain.Entities.Student, Persistence.Entitites.Student>()
                .ForMember(dest => dest.Weight, opt => opt.Ignore())
                .ForMember(dest => dest.Height, opt => opt.Ignore())
                .ForMember(dest => dest.StudentCourses, opt => opt.Ignore());

            CreateMap<Persistence.Entitites.Student, Domain.Entities.Student>();

            CreateMap<Domain.Entities.StudentAddress, Persistence.Entitites.StudentAddress>()
                .ForMember(dest => dest.Student, opt => opt.Ignore())
                .ForMember(dest => dest.StudentId, opt => opt.Ignore());

            CreateMap<Persistence.Entitites.StudentAddress, Domain.Entities.StudentAddress>();
        }
    }
}

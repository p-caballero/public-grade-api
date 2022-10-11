namespace GradeApi.Infrastructure.Mappers
{
    using AutoMapper;

    public class DomainPersistenceProfile : Profile
    {
        public DomainPersistenceProfile()
            : base(nameof(DomainPersistenceProfile))
        {
            CreateMap<Domain.Entities.Student, Persistence.Entitites.Student>();

            CreateMap<Persistence.Entitites.Student, Domain.Entities.Student>();
        }
    }
}

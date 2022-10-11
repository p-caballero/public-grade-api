namespace UnitTests
{
    using AutoMapper;
    using GradeApi;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using IAutomapperGlobalConfiguration = AutoMapper.Internal.IGlobalConfiguration;

    public class AutommaperTests
    {
        private static readonly IAutomapperGlobalConfiguration _mapperConfiguration;

        public static IEnumerable<object[]> ProfileNames { get; }

        static AutommaperTests()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Program).Assembly));
            ProfileNames = _mapperConfiguration.Profiles.Select(x => new object[] { x.Name });
        }

        [Theory]
        [MemberData(nameof(ProfileNames))]
        public void MapperConfiguration_IsValid(string profileName)
        {
            // Act
            _mapperConfiguration.AssertConfigurationIsValid(profileName);
        }
    }
}
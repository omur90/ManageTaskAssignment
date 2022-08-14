using AutoMapper;

namespace ManageTaskAssignment.Employee.Api.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazzy = new(() => {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<CustomMapper>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazzy.Value;
    }
}

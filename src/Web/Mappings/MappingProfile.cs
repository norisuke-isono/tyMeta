using System;
using System.Linq;
using System.Reflection;
using ApplicationCore.Entites;
using AutoMapper;
using Web.Interfaces;

namespace Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i == typeof(IMapping)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
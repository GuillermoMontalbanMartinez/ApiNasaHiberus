using Asteroides.Models;
using AutoMapper;
using System.ComponentModel.Design;

namespace Asteroides.Mappers
{
    public class AsteroideProfile : Profile
    {
        public AsteroideProfile() 
        {
            CreateMap<Asteroide, AsteroideDto>()
                .ForMember(destination => destination.Nombre,
                            source => source.MapFrom(src => $"{src.Name}"))
                .ForMember(destination => destination.DiametroMetros ,
                           source => source.MapFrom(src => $"{Convert.ToDouble((src.EstimatedDiameter.Meters.EstimatedDiameterMax + src.EstimatedDiameter.Meters.EstimatedDiameterMin) / 2)}"))
                .ForMember(destination => destination.Velocidad,
                           source => source.MapFrom(src => $"{src.CloseApproachData.Select(x => x.RelativeVelocity.KilometersPerHour).FirstOrDefault()}"))
                .ForMember(destination => destination.Fecha,
                           source => source.MapFrom(src=> $"{src.CloseApproachData.Select(x => x.CloseApproachDate).FirstOrDefault()}"))
                .ForMember(destination => destination.Planeta,
                           source => source.MapFrom(src=>$"{src.CloseApproachData.Select(x => x.OrbitingBody).FirstOrDefault()}")
                ).ReverseMap();    
        }  
        
    }
}


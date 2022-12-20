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
                .ForMember(destination => destination.DiametroMetros,
                           source => source.MapFrom(src => $"{src.CloseApproachData.Select(x => x.RelativeVelocity).FirstOrDefault()}"))
                .ForMember(destination => destination.Fecha,
                           source => source.MapFrom(src=> $"{src.CloseApproachData.Select(x => x.CloseApproachDate)}"))
                .ForMember(destination => destination.Planeta,
                           source => source.MapFrom(src=>$"{src.CloseApproachData.Select(x => x.OrbitingBody)}")
                ).ReverseMap();    
        }  
        
    }
}


/*
CreateMap<AsteroideDto, Asteroide>()
.ForMember(
      dest => dest.Name,
      opt => opt.MapFrom(src => $"{src.Nombre}"))
 .ForMember(
       dest => (dest.EstimatedDiameter.Meters.EstimatedDiameterMax + dest.EstimatedDiameter.Meters.EstimatedDiameterMin) / 2,
       opt => opt.MapFrom(src => $"{src.DiametroMetros}"))
 .ForMember(
        dest => dest.CloseApproachData.Select(x => x.RelativeVelocity).FirstOrDefault(),
        opt => opt.MapFrom(src => $"{src.DiametroMetros}"))
 .ForMember(
        dest => dest.CloseApproachData.Select(x => x.CloseApproachDate),
        opt => opt.MapFrom(src => $"{src.Fecha}"))
 .ForMember(
        dest => dest.CloseApproachData.Select(x => x.OrbitingBody) ,
        opt => opt.MapFrom(src => $"{src.Planeta}")
  );
*/
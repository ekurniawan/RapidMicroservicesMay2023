using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SampleASPData.DTO;
using SampleASPData.Models;

namespace SampleASPData.Profiles
{
    public class SamuraiProfile : Profile
    {
        public SamuraiProfile()
        {
            CreateMap<Samurai, SamuraiReadDto>();
            CreateMap<SamuraiInsertDto, Samurai>();
            CreateMap<Quote, QuoteWithSamuraiDto>();
        }
    }
}
using AutoMapper;
using FIMDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMDB.AutoMapperConfigurations.Profiles
{
    class FimdbProfile : Profile
    {
        public FimdbProfile()
        {
            CreateMap<Movie, MovieDetailed>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => getSplittedArray(src.Genre)))
                .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => getSplittedArray(src.Stars)))
                .ForMember(dest => dest.Directors, opt => opt.MapFrom(src => getSplittedArray(src.Directors)));
        }
        private string[] getSplittedArray(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            return s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

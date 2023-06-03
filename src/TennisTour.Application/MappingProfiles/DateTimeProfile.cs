using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTour.Application.MappingProfiles
{
    public class DateTimeProfile : Profile
    {
        public DateTimeProfile()
        {
            CreateMap<DateTime?, DateTime>()
                .ConvertUsing(dt => dt ?? default);
        }
    }
}

using FIMDB.AutoMapperConfigurations.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMDB.AutoMapperConfigurations
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new FimdbProfile());
            });

        }
    }
}

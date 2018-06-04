using System.Collections.Generic;

namespace Farzin.Infrastructure.CrossCutting.Mapper
{
    public class Mapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
                return default(TDestination);

            var result = AutoMapper.Mapper.Map<TSource, TDestination>(source);
            return result;
        }
        public static List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
        {
            if (source == null)
                return null;

            var result = AutoMapper.Mapper.Map<List<TDestination>>(source);
            return result;
        }
        public static TDestination Map<TDestination>(object source)
        {
            if (source == null)
                return default(TDestination);

            return AutoMapper.Mapper.Map<TDestination>(source);
        }
    }
}

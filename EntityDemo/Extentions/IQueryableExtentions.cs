using EntityDemo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EntityDemo.Extentions
{
    public static class IQueryableExtentions
    {
        public static IQueryable<T> Pagination<T>(this IQueryable<T> source, int pageno, int recordToTake)
        {
            return source.Skip((pageno - 1)*recordToTake).Take(recordToTake);
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> source, FilterDto filterDto)
        {
            Type type = typeof(T);

            source.Skip((filterDto.Page - 1) * filterDto.recordToTake).Take(filterDto.recordToTake);

            //foreach (var obj in filterDto.Filters)
            //{
            //    PropertyInfo info = type.GetProperty(obj.field);
            //    if (info != null)
            //    {
            //        if (obj.operation == "startwith")
            //        {
            //            source = source.Where(m => m[obj.field].ToString().StartsWith(obj.value));
            //        }
            //        else if(obj.operation == "contain")
            //        {
            //            source = source.Where(m => info.GetValue(m).ToString().Contains(obj.value));
            //        }
            //        else if(obj.operation == "endwith")
            //        {
            //            source = source.Where(m => info.GetValue(m).ToString().EndsWith(obj.value));
            //        }
            //    }
            //}

            return source;
        }
    }
}
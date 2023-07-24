using EntityDemo.Context;
using EntityDemo.DTO;
using System.Reflection;

namespace EntityDemo.Helper
{
    public class FilterClass<T>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FilterClass(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

        //public void DoFilter(FilterDto filterDto)
        //{
        //    Type type = typeof(T);
        //    var Queryable = _applicationDbContext.Set<T>().AsQueryable();

        //    foreach (var obj in filterDto.Filters)
        //    {
        //        PropertyInfo info = type.GetProperty(obj.field);
        //        if (info != null)
        //        {
        //            if(obj.operation == "startwith")
        //            {
        //                Queryable = Queryable.Where(m => info.GetValue(m).ToString().StartsWith(obj.value));
        //            }
        //        }
        //    }

           

        //}

    }
}

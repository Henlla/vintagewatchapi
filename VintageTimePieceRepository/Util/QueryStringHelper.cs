using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VintageTimePieceRepository.Util
{
    public static class QueryStringHelper
    {
        public static string? ToQueryString<T>(this T entity)
        {
            if (entity == null)
            {
                return string.Empty;
            }
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                      .Where(p => p.GetValue(entity) != null);
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            foreach (var property in properties)
            {
                var value = property.GetValue(entity);
                if (value is IEnumerable<string> list)
                {
                    foreach (var item in list)
                    {
                        queryString.Add(property.Name, item);
                    }
                }
                else
                {
                    queryString.Add(property.Name, value.ToString());
                }
            }

            return queryString.ToString();
        }
    }
}

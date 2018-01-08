using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Passport.Business.Extension
{
    public static class QueryableExtension
    {
        public static void DeleteMany<TEntity>(this DbSet<TEntity> source, Func<TEntity, bool> predicate) where TEntity : class
        {
            source.Where(predicate)
                  .ToList()
                  .ForEach(entity =>
                  {
                      source.Remove(entity);
                  });
        }
    }
}
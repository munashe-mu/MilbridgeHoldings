
using MilbridgeHoldings.Validations.Extensions;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public class CrudRepository<T> : ICrudRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public CrudRepository(ApplicationDbContext context) => _context = context;

        public T Add(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Delete<TKey>(TKey id)
        {
            var dbItem = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(dbItem);
            _context.SaveChanges();
        }

        public IEnumerable<T> Find() => _context.Set<T>();

        public T Find<TKey>(TKey id) => _context.Set<T>().Find(id);

        public T Update<TKey>(TKey id, T item)
        {
            var dbItem = _context.Set<T>().Find(id);
            if (dbItem == null) throw new System.Exception("Could not find requested item");
            var props = item.GetType().GetProperties();
            foreach (var prop in props)
            {
                object propValue = prop.GetValue(item);
                if (propValue != null && propValue.GetType().IsPrimitive())
                    prop.SetValue(dbItem, propValue);
            }
            _context.SaveChanges();
            return item;
        }
    }
}

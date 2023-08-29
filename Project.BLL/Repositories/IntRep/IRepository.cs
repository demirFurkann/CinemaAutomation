using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Repositories.IntRep
{
    public interface IRepository<T> where T : BaseEntity
    {
        // Listeleme işlemleri
        List<T> GetAll();
        List<T> GetActives();
        List<T> GetPassives();
        List<T> GetModifieds();

        // Crud işlemleri

        void Add(T item);
        void AddRange(List<T> list);
        void Update(T item);
        void Delete(T item);
        void Destroy(T item);
        void DeleteRange(List<T> list);
        void UpdateRange(List<T> lit);


        // Linq sorgulari

        List<T> Where(Expression<Func<T, bool>> exp);
        bool Any(Expression<Func<T, bool>> exp);
        T FirstOrDefault(Expression<Func<T, bool>> exp);
        object Select(Expression<Func<T, object>> exp);
        IQueryable<X> Select<X>(Expression<Func<T, X>> exp);

        // Find

        T Find(int id);

    }
}

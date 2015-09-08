using SAD360.Application.Interfaces;
using SAD360.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Application
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public void Add(TEntity obj)
        {
            this._serviceBase.Add(obj);
        }

        public TEntity GetById(int id)
        {
            return this._serviceBase.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this._serviceBase.GetAll();
        }

        public void Update(TEntity obj)
        {
            this._serviceBase.Update(obj);
        }

        public void Remove(TEntity obj)
        {
            this._serviceBase.Remove(obj);
        }
    }
}

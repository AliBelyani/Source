using AutoMapper;
using DK.Service.Interface.Repository;
using System;

namespace DK.Service.Service
{
    public class BaseService: IDisposable
    {
        internal readonly IUnitOfWork uow;
        internal readonly IMapper _IMapper;
        public BaseService(IUnitOfWork uow, IMapper IMapper)
        {
            this.uow = uow;
            this._IMapper = IMapper;
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediatorPattern.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Add(T item);
        Task Edit(T item);
        Task Delete(int id);
        Int64 MaiorId();
    }
}
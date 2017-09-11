using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Example.Models;
using Example.Repositories.Interfaces;

namespace Example.Services.Tests.Fakes
{
    internal class FakeColorRepository : ICachedRepository<Color>
    {
        public List<Color> Colors = new List<Color>();

        public bool GetCalled { get; set; }
        public bool GetAllCalled { get; private set; }

        public Color Get(Func<Color, bool> predicate)
        {
            GetCalled = true;
            return Colors.SingleOrDefault(predicate);
        }

        public IQueryable<Color> GetAll()
        {
            GetAllCalled = true;
            return Colors.AsQueryable();
        }

        public void Add(Color entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IRepository<Color> Include(Expression<Func<Color, object>> path)
        {
            throw new NotImplementedException();
        }
    }
}
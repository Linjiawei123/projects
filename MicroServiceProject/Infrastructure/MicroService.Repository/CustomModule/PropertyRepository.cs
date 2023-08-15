using MicroService.Interfaces;
using MicroService.Models.CustomModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Repository.CustomModule
{
    public class PropertyRepository : DataContext, IPropertyRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<PropertyValue> _propertieSet;
        public PropertyRepository(DataContext context)
        {
            _context = context;
            _propertieSet = _context.Set<PropertyValue>();
        }
    }
}

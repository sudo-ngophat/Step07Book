﻿using Booking.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Interface
{
    public interface IVillaRepository : IRepository<Villa>
    {

        void Update(Villa entity);

       
    }
}

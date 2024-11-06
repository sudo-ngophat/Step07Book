﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Interface
{
    public interface IUnitOfWork
    {
        IVillaRepository? Villa { get; }
        IVillaNumberRepository VillaNumber { get; }
        void Save();
    }
}

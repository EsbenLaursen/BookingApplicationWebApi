﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAL
{
    public class BookingDbInitializer : DropCreateDatabaseIfModelChanges<BookingDbContext>
    {
        protected override void Seed(BookingDbContext context)
        {
            base.Seed(context);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SignalROrnek
{
    public class SignalRContext : DbContext
    {
        //Context base alınıyor . Bağlantı dizesi 
        public SignalRContext() : base("SignalRConnection")
        {
            
        }

        public DbSet<SignalRVeri> SignalRTest { get; set; }
    }
}

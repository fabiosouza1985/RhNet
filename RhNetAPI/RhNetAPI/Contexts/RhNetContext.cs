using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RhNetAPI.Entities.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Contexts
{
    public class RhNetContext : IdentityDbContext<ApplicationUser>
    {
       public RhNetContext(DbContextOptions <RhNetContext> options): base(options) { }
    }
}

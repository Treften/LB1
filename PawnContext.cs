using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace LB1
{
    class PawnContext:DbContext
    {
        public PawnContext() : base("DbConnection") { }
        public DbSet<Pawn> Pawns { get; set; }
    }
}

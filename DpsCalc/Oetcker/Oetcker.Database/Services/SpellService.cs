using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oetcker.Models.Models;

namespace Oetcker.Data.Services
{
    public static class SpellService
    {

        public static Spell GetSpells(int spellId)
        {
            using (var dbContext = new Database())
            {
                return dbContext.Spells.FirstOrDefault(spell => spell.SpellIdent == spellId);
            }
        }

        public static void WriteSpells(List<Spell> spells)
        {
            using (var dbContext = new Database())
            {
                dbContext.Spells.RemoveRange(dbContext.Spells);
                dbContext.Spells.AddRange(spells);
                dbContext.SaveChanges();
            }
        }
    }
}

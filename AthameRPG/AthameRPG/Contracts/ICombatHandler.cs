using AthameRPG.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthameRPG.Contracts
{
    public interface ICombatHandler
    {
        Unit Unit { get; set; }

        IAtack GenerateAtack();
    }
}

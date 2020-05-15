using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// ICombat.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Combat interface used on objects that can attack and be attacked.
    /// </summary>
    public interface ICombat {

        bool Attack(Actor actor);
    }
}

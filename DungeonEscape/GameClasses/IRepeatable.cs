using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses {

    /// <summary>
    /// IRepeatable.cs
    /// Written By: Marcus Goeckner
    /// 3/1/19
    /// Description: Repeatable interface for items that can have several things of the same type on a map.
    /// </summary>
    public interface IRepeatable<T> {

        T CreateCopy();
    }
}

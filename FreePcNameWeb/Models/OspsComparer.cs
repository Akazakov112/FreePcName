using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FreePcNameWeb.Models
{
    /// <summary>
    /// Компаратор для ОСП.
    /// </summary>
    public class OspsComparer : IEqualityComparer<Osps>
    {
        public bool Equals([AllowNull] Osps first, [AllowNull] Osps second)
        {
            if (second == null && first == null)
                return true;
            else if (first == null || second == null)
                return false;
            else if (first.Id == second.Id)
                return true;
            else if (first.Osp == second.Osp)
                if (first.Address == second.Address)
                    return true;
                else
                    return false;
            else
                return false;
        }

        public int GetHashCode([DisallowNull] Osps obj)
        {
            return base.GetHashCode();
        }
    }
}

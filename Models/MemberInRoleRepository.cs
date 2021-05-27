using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricalShop.Models
{
    public class MemberInRoleRepository: BaseRepository
    {
        public int Add(MemberInRole obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name ="@MemberId", Value= obj.MemberId, DbType = DbType.AnsiString},
                 new Parameter{ Name = "@RoleId", Value = obj.RoleId, DbType = DbType .Int32 }
            };
            return Save("AddMemberInRole", parameters);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_market.Core.Application.Custom_Data_Annotations
{
    class MaxRange4 : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list == null)
            {
                return true;
            }
            else if (list != null)
            {
                return list.Count <= 4 && list.Count > 0;
            }
            return false;
        }
    }
}

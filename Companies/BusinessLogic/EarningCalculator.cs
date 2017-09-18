using System;
using System.Collections.Generic;
using System.Linq;
using Companies.Models;

namespace Companies.BusinessLogic
{
    public static class EarningCalculator
    {
        public static int GetTotalEarnings(List<Company> data, Guid? parentId)
        {
            var items = data.Where(d => d.ParentId == parentId);
            var total = 0;
            if (items.Any())
            {
                foreach (var item in items)
                {
                        
                    var childItems = data.Where(d => d.ParentId == item.CompanyId);
                    if (childItems.Any())
                    {
                        item.TotalEarning = GetTotalEarnings(data, item.CompanyId) + item.Earning;
                        total += item.TotalEarning.Value;
                    }
                    else
                    {
                        total += item.Earning;
                    }
                }
            }
            return total;
        }
    }
}
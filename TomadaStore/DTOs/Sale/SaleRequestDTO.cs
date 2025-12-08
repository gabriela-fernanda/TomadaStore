using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleRequestDTO
    {
        public List<string> ProductIds { get; set; } = new List<string>();
    }
}

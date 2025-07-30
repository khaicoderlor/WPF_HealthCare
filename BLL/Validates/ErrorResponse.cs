using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validates
{
    public class ErrorResponse
    {
        public bool IsError { get; set; } = false;    

        public List<string> Errors { get; set; } = new List<string>();

    }
}

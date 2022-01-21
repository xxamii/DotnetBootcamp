using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class TestInfo
    {
        public bool IsSuccessful { get; set; } = true;

        public string Message { get; set; } = "Test run successfully";

        public string TestName { get; set; }
    }
}

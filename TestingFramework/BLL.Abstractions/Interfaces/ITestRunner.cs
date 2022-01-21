using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;

namespace BLL.Abstractions.Interfaces
{
    public interface ITestRunner
    {
        List<TestInfo> RunTests();
    }
}

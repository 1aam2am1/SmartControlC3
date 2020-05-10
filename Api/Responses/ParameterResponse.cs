using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Responses
{
    public class ParameterResponse
    {
        public Dictionary<int, int> Parameters { set; get; } = new Dictionary<int, int>();
    }
}

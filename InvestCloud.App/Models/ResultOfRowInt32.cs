using System.Collections.Generic;

namespace InvestCloud.App.Models
{
    public class ResultOfRowInt32
    {
        public List<int> Value { get; set; }
        public string Cause { get; set; }
        public bool Success { get; set; }
    }
}

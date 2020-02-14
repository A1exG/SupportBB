using System;

namespace SupBB.Controller
{
    public class MyPC
    {
        public string PcName()
        {
            var _pcName = Environment.MachineName;
            return _pcName;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace L3568
{
    interface ISeries
    {
        string GetSeries();
        void JoinSeries(string seriesName);
        void LeaveSeries();
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Syncfusion.Licensing;

namespace Novea
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhkWFpGaV1EQmFJfFBmTGlbeVR0cUU3HVdTRHRcQlxhTn5ac0ZhXHtbcXI=;Mgo+DSMBPh8sVXJ0S0J+XE9AclRAQmFPYVF2R2BJdlRycl9CZUwgOX1dQl9gSX9RcUVqXHxcdXBSTmY=;ORg4AjUWIQA/Gnt2VVhkQlFacltJX3xLeEx0RWFab1d6dlNMZFlBNQtUQF1hSn5SdEJjUHpdcnFRT2Fa;MTAxNjc1NEAzMjMwMmUzNDJlMzBCOENRSUZLcnhGekIvWHh0bjZCekFTRjN0ZFEzSllpS0pNTFp4UTB6Y1d3PQ==;MTAxNjc1NUAzMjMwMmUzNDJlMzBnMXhFeEMxY2l2am1zOXNlbTdsSjN6WUYrcDI5T2QxcmtZRjViZFlhYk9jPQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFtGVmFWfFVpR2NbfE55fldAalhUVAciSV9jS31Td0dkWXZbc3ZQQ2hcVg==;MTAxNjc1N0AzMjMwMmUzNDJlMzBoVjhhNDg4cHNqY0w5L0hJMHZ3a0R3eWxEdFdoNG9pUmZ5QllYcjFaZWNFPQ==;MTAxNjc1OEAzMjMwMmUzNDJlMzBrY3NJczJNQnd0djlwQ2VXWkowYyt5WGhjTlFVVFE1a0d5Si85aVU5UWM0PQ==;Mgo+DSMBMAY9C3t2VVhkQlFacltJX3xLeEx0RWFab1d6dlNMZFlBNQtUQF1hSn5SdEJjUHpdcnFTTmVa;MTAxNjc2MEAzMjMwMmUzNDJlMzBhYzRuOFh3a0JobFBBSmJ6V251eHhKRXhBSHhRYUVHRGNGS1I3bGdwK3BjPQ==;MTAxNjc2MUAzMjMwMmUzNDJlMzBYTHN1Q0ZkeXBQQ04wOXpGQVAwaWNEQWtBODlQRk5kRm5jZXVNYkdnS1JzPQ==;MTAxNjc2MkAzMjMwMmUzNDJlMzBoVjhhNDg4cHNqY0w5L0hJMHZ3a0R3eWxEdFdoNG9pUmZ5QllYcjFaZWNFPQ==");
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }
    }
}

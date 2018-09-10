using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsWebApplication.Markup
{
    public class ActivityMarkup:IMarkups
    {
        public double GetMarkup(double fare)
        {
            return (fare + fare * 0.4F);
        }
    }
}
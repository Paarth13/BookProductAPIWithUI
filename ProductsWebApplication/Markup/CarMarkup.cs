using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsWebApplication.Markup
{
    public class CarMarkup : IMarkups
    {
        public double GetMarkup(double fare)
        {
            return (fare + fare * 0.1F);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using DataAccess;
using System.Web.Http;
using ProductsWebApplication.Models;
using ProductsWebApplication.Markup;

namespace ProductsWebApplication.Controllers
{
    public class AirController : ApiController
    {
        public IEnumerable<Air> GetValues()
        {
            using (ProductDataBaseEntities entity = new ProductDataBaseEntities())
            {
                IEnumerable<Air> airs = entity.Airs.ToList();
                IMarkups markup = new AirMarkup();
                foreach (Air air in airs)
                {
                    air.Price = markup.GetMarkup(Convert.ToDouble(air.Price));
                }
                return airs;
            }

        }


        public IEnumerable<Air> GetValues(string types)
        {

            using (ProductDataBaseEntities entity = new ProductDataBaseEntities())
            {
                if (types.Equals("booked"))
                {
                    var query = from value in entity.Airs
                                where value.IsBooked == false
                                select value;
                    return query.ToList();
                }
                else
                {
                    var query = from value in entity.Airs
                                where value.IsSaved == false
                                select value;
                    return query.ToList();
                }
            }

        }

        [HttpPut]
        public void PutValues(SettingValue item)
        {
            using (ProductDataBaseEntities entity = new ProductDataBaseEntities())
            {
                if (item.type == "booked")
                {

                    entity.Airs.Find(item.id).IsBooked = true;
                    entity.SaveChanges();
                }
                else
                {
                    entity.Airs.Find(item.id).IsSaved = true;
                    entity.SaveChanges();
                }
            }

        }

        [HttpPost]
        public void Insertion([FromBody]Air air)
        {
            using (ProductDataBaseEntities entity = new ProductDataBaseEntities())
            {
                entity.Airs.Add(air);
                entity.SaveChanges();
            }

        }
    }
}

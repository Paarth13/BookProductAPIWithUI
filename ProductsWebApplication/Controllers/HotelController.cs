using System;
using DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsWebApplication.Models;
using ProductsWebApplication.Markup;

namespace ProductsWebApplication.Controllers
{
    public class HotelController : ApiController
    {
        public IEnumerable<Hotel> GetValues()
        {
            using (ProductDataBaseEntities entity = new ProductDataBaseEntities())
            {
                IEnumerable<Hotel> hotels = entity.Hotels.ToList();
                IMarkups markup = new HotelMarkup();
                foreach (Hotel hotel in hotels)
                {
                    hotel.Price = markup.GetMarkup(Convert.ToDouble(hotel.Price));
                }
                return hotels;
            }

        }


        public IEnumerable<Hotel> GetValues(string types)
        {

            using (ProductDataBaseEntities entity = new ProductDataBaseEntities())
            {
                if (types.Equals("booked"))
                {
                    var query = from value in entity.Hotels
                                where value.IsBooked == false
                                select value;
                    return query.ToList();
                }
                else
                {
                    var query = from value in entity.Hotels
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

                    entity.Hotels.Find(item.id ).IsBooked = true;
                    entity.SaveChanges();
                }
                else
                {
                    entity.Hotels.Find(item.id).IsSaved = true;
                    entity.SaveChanges();
                }
            }

        }

        [HttpPost]
        public void Insertion([FromBody]Hotel hot)
        {
            using (ProductDataBaseEntities entity = new ProductDataBaseEntities())
            {
                entity.Hotels.Add(hot);
                entity.SaveChanges();
            }

        }
    }


   


}

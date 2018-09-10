using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ProductsWebApplication.Models;
using System.Net.Http;
using System.Web.Http;
using ProductsWebApplication.Markup;

namespace ProductsWebApplication.Controllers
{
    public class CarController : ApiController
    {
        public IEnumerable<Car> GetValues()
        {
            using (ProductDataBaseEntities entity = new ProductDataBaseEntities())
            {
                IEnumerable<Car> cars = entity.Cars.ToList();
                IMarkups markup = new CarMarkup();
                foreach(Car car in cars)
                {
                    car.Price = markup.GetMarkup(Convert.ToDouble(car.Price));
                }
                return cars;
            }   
                
        }


        public IEnumerable<Car> GetValues(string types)
        {

            using (ProductDataBaseEntities entity = new ProductDataBaseEntities())
            {
                if (types.Equals("booked"))
                {
                    var query = from value in entity.Cars
                                where value.IsBooked == false
                                select value;
                    return query.ToList();
                }
                else
                {
                    var query = from value in entity.Cars
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

                    entity.Cars.Find(item.id).IsBooked = true;
                    entity.SaveChanges();
                }
                else
                {
                    entity.Cars.Find(item.id).IsSaved = true;
                    entity.SaveChanges();
                }
            }

        }

        [HttpPost]
        public void Insertion([FromBody]Car car)
        {
            using (ProductDataBaseEntities entity = new ProductDataBaseEntities())
            {
                entity.Cars.Add(car);
                entity.SaveChanges();
            }

        }
    }
}

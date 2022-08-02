using AutoLab.Domain.Entities;
using AutoLab.Utils.Bases;
using AutoLab.Utils.Delegates;
using System;
using System.Linq.Expressions;

namespace AutoLab.Application.Parameters
{
    public class CarModelParams : BaseParams<CarModel>
    {
        public string Key { get; set; }
        public string KeyCarBrand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool Removed { get; set;}
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }


        public override Expression<Func<CarModel, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<CarModel>();
            predicate = predicate.And(p => p.Removed.Equals(Removed));


            if (!string.IsNullOrWhiteSpace(Key))
            {
                var carModel = new CarModel { Key = Key };
                predicate = predicate.And(p => p.Id.Equals(carModel.Id));
            }

            if (!string.IsNullOrWhiteSpace(KeyCarBrand))
            {
                var carBrand = new CarBrand { Key = KeyCarBrand };
                predicate = predicate.And(p => p.IdCarBrand == carBrand.Id);
            }

            if (Id.HasValue)
            {
                predicate = predicate.And(p => p.Id == Id);
            }

            if (!string.IsNullOrWhiteSpace(Model))
            {
                predicate = predicate.And(p => p.Model.Equals(Model, StringComparison.CurrentCultureIgnoreCase));
            }

            if (Year > 0)
            {
                predicate = predicate.And(p => p.Year == Year);
            }

            if (Created != null && Created != DateTime.MinValue)
            {
                predicate = predicate.And(p => p.Created == Created);
            }

            if (Updated != null && Updated != DateTime.MinValue)
            {
                predicate = predicate.And(p => p.Updated == Updated);
            }

            return predicate;
        }
    }
}

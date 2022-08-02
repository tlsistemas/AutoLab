using AutoLab.Domain.Entities;
using AutoLab.Utils.Bases;
using AutoLab.Utils.Delegates;
using System.Linq.Expressions;

namespace AutoLab.Application.Parameters
{
    public class CarBrandParams : BaseParams<CarBrand>
    {
        public string? Key { get; set; }
        
        public string? Brand { get; set; }
        public bool Removed { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }


        public override Expression<Func<CarBrand, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<CarBrand>();
            predicate = predicate.And(p => p.Removed.Equals(Removed));

            if (!string.IsNullOrWhiteSpace(Key))
            {
                var carBrand = new CarBrand { Key = Key };
                predicate = predicate.And(p => p.Id.Equals(carBrand.Id));
            }

            if (Id.HasValue)
            {
                predicate = predicate.And(p => p.Id == Id);
            }

            if (!string.IsNullOrWhiteSpace(Brand))
            {
                predicate = predicate.And(p => p.Brand.Equals(Brand, StringComparison.CurrentCultureIgnoreCase));
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

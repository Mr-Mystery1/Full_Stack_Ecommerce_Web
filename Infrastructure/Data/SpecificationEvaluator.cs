﻿using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBY != null)
            {
                query = query.OrderBy(spec.OrderBY);
            }

            if (spec.OrderBYDescending != null)
            {
                query = query.OrderByDescending(spec.OrderBYDescending);
            }

            if(spec.IsDistinct)
            {
                query = query.Distinct();
            }
            return query;
        }







        public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, ISpecification<T, TResult> spec)
        {
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBY != null)
            {
                query = query.OrderBy(spec.OrderBY);
            }

            if (spec.OrderBYDescending != null)
            {
                query = query.OrderByDescending(spec.OrderBYDescending);
            }

            var selectQuery = query as IQueryable<TResult>;

            if(spec.Select != null)
            {
                selectQuery = query.Select(spec.Select);

            }

            if (spec.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }

            return selectQuery ?? query.Cast<TResult>();
        }
    }
}

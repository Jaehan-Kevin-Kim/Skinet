using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }


        // 아래는 pagination 위한 설정 값들
        int Take { get; } // 몇개의 값을 가져올 지 
        int Skip { get; } // 몇개의 값을 건너띌 지
        bool IsPagingEnabled { get; }
    }
}
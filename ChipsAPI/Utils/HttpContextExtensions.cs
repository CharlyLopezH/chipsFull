using Microsoft.EntityFrameworkCore;

namespace AnticorAPI.Utils
{
    //Esta clase ayuda a centralizar la lógica de la paginación
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosPaginacionEnCabecera<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            double cantidad = await queryable.CountAsync();
            httpContext.Response.Headers.Append("cantidadTotalRegistros", cantidad.ToString());
        }

    }
}


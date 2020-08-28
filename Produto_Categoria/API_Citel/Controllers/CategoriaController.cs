using API_Citel.Application;
using Microsoft.AspNetCore.Mvc;

namespace API_Citel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private CategoriaApplication categoriaApplication;

        public CategoriaController(CategoriaApplication _categoriaApplication)
        {
            categoriaApplication = _categoriaApplication;
        }
    }
}

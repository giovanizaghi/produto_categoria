using API_Citel.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Citel.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private ProdutoApplication produtoApplication;

        public ProdutoController(ProdutoApplication _produtoApplication)
        {
            produtoApplication = _produtoApplication;
        }
    }
}

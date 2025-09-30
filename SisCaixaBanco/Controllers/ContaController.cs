using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using SisCaixaBanco.DTO;
using SisCaixaBanco.Resources;
using SisCaixaBanco.Services;

namespace SisCaixaBanco.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;
        private readonly ITransferenciaService _transferenciaService;
        
        public ContaController(IContaService contaService, ITransferenciaService transferenciaService)
        {
                _contaService = contaService;
                _transferenciaService = transferenciaService;
        }

        /// <summary>
        /// Cria uma nova conta
        /// POST: api/CriarConta
        /// </summary>
        [HttpPost("criar-conta")]
        public async Task<IActionResult> CriarConta([FromBody] ContaCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var conta = await _contaService.CriarContaAsync(dto);

                var result = new ContaDTO
                {
                    NomeCliente = conta.NomeCliente,
                    Documento = conta.Documento,
                    Saldo = conta.Saldo,
                    DataAbertura = conta.DataAbertura,
                    StatusDaConta = conta.Status.GetDisplayName()
                };

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest( new { message = ex.Message });
            }
        }
        /// <summary>
        /// Consulta uma conta por nome ou documento
        /// GET: api/ConsultarConta
        /// </summary>
        [HttpGet("consultar-conta")]
        public async Task<IActionResult> ConsultarConta([FromQuery] string? nome, [FromQuery] string? documento)
        {
            try
            {
                var contas = await _contaService.ListarContasAsync(nome, documento);

                var result = contas.Select(conta => new ContaDTO
                {
                    NomeCliente = conta.NomeCliente,
                    Documento = conta.Documento,
                    Saldo = conta.Saldo,
                    DataAbertura = conta.DataAbertura,
                    StatusDaConta = conta.Status.GetDisplayName()
                });

                return Ok(result);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Inativa conta
        /// PUT: api/InativarConta
        /// </summary>
        [HttpPut("inativar-conta")]
        public async Task<IActionResult> InativarConta([FromQuery] string documento)
        {
            try
            {
                await _contaService.InativarContaPorDocumentoAsync(documento);

                return Ok(new { message = GlobalResource.ReturnSucess});
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cria uma nova conta
        /// PUT: api/InativarConta
        /// </summary>
        [HttpPut("transferir")]
        public async Task<IActionResult> Transferir([FromBody] TransferenciaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _contaService.TransferirAsync(dto);

                return Ok(new { message = GlobalResource.ReturnSucess });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
